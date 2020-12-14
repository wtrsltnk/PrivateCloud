using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PrivateCloud.Api.Models;
using PrivateCloud.Application.Users.Commands.AuthenticateUser;
using PrivateCloud.Application.Users.Commands.CreateUser;
using PrivateCloud.Application.Users.Commands.DeleteUser;
using PrivateCloud.Application.Users.Queries.GetAllUsers;
using PrivateCloud.Application.Users.Queries.GetUser;
using PrivateCloud.Domain;
using PrivateCloud.Practises.Executables;
using PrivateCloud.Practises.Logging;
using System;

namespace PrivateCloud.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController :
        ControllerBase
    {
        private readonly ICommand<AuthenticateUserModel, AuthenticateUserResult> _authenticateCommand;
        private readonly ICommand<CreateUserModel, CreateUserResult> _createUserCommand;
        private readonly ICommand<DeleteUserModel> _deleteUserCommand;
        private readonly IQuery<GetAllUsersModel, GetAllUsersResult> _getAllUsersQuery;
        private readonly IQuery<GetUserModel, GetUserResult> _getUserQuery;

        public ILogger Logger { get; set; } = new NullLogger();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="authenticateCommand"></param>
        /// <param name="createUserCommand"></param>
        /// <param name="deleteUserCommand"></param>
        /// <param name="getAllUsersQuery"></param>
        /// <param name="getUserQuery"></param>
        public UsersController(
            ICommand<AuthenticateUserModel, AuthenticateUserResult> authenticateCommand,
            ICommand<CreateUserModel, CreateUserResult> createUserCommand,
            ICommand<DeleteUserModel> deleteUserCommand,
            IQuery<GetAllUsersModel, GetAllUsersResult> getAllUsersQuery,
            IQuery<GetUserModel, GetUserResult> getUserQuery)
        {
            _authenticateCommand = authenticateCommand ?? throw new ArgumentNullException(nameof(authenticateCommand));
            _createUserCommand = createUserCommand ?? throw new ArgumentNullException(nameof(createUserCommand));
            _deleteUserCommand = deleteUserCommand ?? throw new ArgumentNullException(nameof(deleteUserCommand));
            _getAllUsersQuery = getAllUsersQuery ?? throw new ArgumentNullException(nameof(getAllUsersQuery));
            _getUserQuery = getUserQuery ?? throw new ArgumentNullException(nameof(getUserQuery));
        }

        /// <summary>
        /// Authenticate a user and return a token
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate(
            [FromBody] AuthenticateRequest model)
        {
            var authenticateModel = new AuthenticateUserModel()
            {
                Username = model.Username,
                Password = model.Password,
            };

            AuthenticateUserResult result;
            Exception ex;

            if (!_authenticateCommand.TryExecute(authenticateModel, out result, out ex))
            {
                Logger
                    .WithException(ex)
                    .WithField(nameof(model.Username), model.Username)
                    .Error("failed to authenticate");

                return BadRequest(new { message = "Username or password is incorrect" });
            }

            return Ok(result.AuthenticatedUser);
        }

        /// <summary>
        /// Register a new user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register(
            [FromBody] RegisterUserRequest model)
        {
            // map 
            var createUserModel = new CreateUserModel()
            {
                Email = model.Email,
                Firstname = model.FirstName,
                Lastname = model.LastName,
                Password = model.Password,
                Role = Role.User,
                Username = model.Username,
            };

            CreateUserResult result;
            Exception ex;

            if (!_createUserCommand.TryExecute(createUserModel, out result, out ex))
            {
                Logger
                    .WithException(ex)
                    .WithField(nameof(model.Username), model.Username)
                    .Error("failed to register user");

                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }

            return Ok(result.CreatedUser);
        }

        /// <summary>
        /// Delete user by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(
            Guid id,
            string password)
        {
            var getUserModel = new DeleteUserModel()
            {
                Id = id,
                User = User,
                Password = password,
            };

            Exception ex;

            if (!_deleteUserCommand.TryExecute(getUserModel, out ex))
            {
                Logger
                    .WithException(ex)
                    .WithField(nameof(User), User.Identity.Name)
                    .Error("failed to delete user");

                return NotFound();
            }

            return Ok();
        }

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = Role.Admin)]
        [HttpGet]
        public IActionResult GetAll()
        {
            var getAllUsersModel = new GetAllUsersModel()
            {
                User = User,
            };

            GetAllUsersResult result;
            Exception ex;

            if (!_getAllUsersQuery.TryExecute(getAllUsersModel, out result, out ex))
            {
                Logger
                    .WithException(ex)
                    .WithField(nameof(User), User.Identity.Name)
                    .Error("failed to get all users");

                return NotFound();
            }

            return Ok(result.Users);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult GetById(
            Guid id)
        {
            var getUserModel = new GetUserModel()
            {
                Id = id,
                User = User,
            };

            GetUserResult result;
            Exception ex;

            if (!_getUserQuery.TryExecute(getUserModel, out result, out ex))
            {
                Logger
                    .WithException(ex)
                    .WithField(nameof(User), User.Identity.Name)
                    .Error("failed to get user by id");

                return NotFound();
            }

            return Ok(result.User);
        }
    }
}

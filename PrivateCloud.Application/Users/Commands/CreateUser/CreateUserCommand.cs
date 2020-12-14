using PrivateCloud.Application.Interfaces;
using System;

namespace PrivateCloud.Application.Users.Commands.CreateUser
{
    public partial class CreateUserCommand
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateUserCommand(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        protected override CreateUserResult InternalExecute(
            CreateUserModel model)
        {
            var result = _unitOfWork
                .Users
                .Add(new Domain.Users.User()
                {
                    Email = model.Email,
                    FirstName = model.Firstname,
                    LastName = model.Lastname,
                    Role = model.Role,
                    Username = model.Username,
                });

            _unitOfWork
                .Save();

            _unitOfWork
                .SetUserPassword(
                    result.Id,
                    model.Password);

            _unitOfWork
                .Save();

            return new CreateUserResult()
            {
                CreatedUser = result,
            };
        }
    }
}

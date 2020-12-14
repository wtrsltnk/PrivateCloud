using PrivateCloud.Practises.Executables;
using PrivateCloud.Practises.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PrivateCloud.Domain.Users;

namespace PrivateCloud.Application.Users.Commands.AuthenticateUser
{
    public partial class AuthenticateUserModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public partial class AuthenticateUserResult
    {
        public User AuthenticatedUser { get; set; }
    }

    public partial class AuthenticateUserCommand :
        AbstractCommand<AuthenticateUserModel, AuthenticateUserResult>
    {
        public ILogger Logger { get; set; } = new NullLogger();
    }
}

namespace PrivateCloud.Application.Users.Commands.CreateUser
{
    public partial class CreateUserModel
    {
        [Required]
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Role { get; set; }
        [Required]
        public string Email { get; set; }
    }

    public partial class CreateUserResult
    {
        public User CreatedUser { get; set; }
    }

    public partial class CreateUserCommand :
        AbstractCommand<CreateUserModel, CreateUserResult>
    {
        public ILogger Logger { get; set; } = new NullLogger();
    }
}

namespace PrivateCloud.Application.Users.Commands.DeleteUser
{
    public partial class DeleteUserModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public System.Security.Claims.ClaimsPrincipal User { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public partial class DeleteUserCommand :
        AbstractCommand<DeleteUserModel>
    {
        public ILogger Logger { get; set; } = new NullLogger();
    }
}

namespace PrivateCloud.Application.Users.Commands.ResetPassword
{
    public partial class ResetPasswordModel
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public System.Security.Claims.ClaimsPrincipal User { get; set; }
    }

    public partial class ResetPasswordCommand :
        AbstractCommand<ResetPasswordModel>
    {
        public ILogger Logger { get; set; } = new NullLogger();
    }
}

namespace PrivateCloud.Application.Users.Commands.RecoverPassword
{
    public partial class RecoverPasswordModel
    {
        [Required]
        public string RecoveryToken { get; set; }
    }

    public partial class RecoverPasswordResult
    {
        public User RecoverdUser { get; set; }
    }

    public partial class RecoverPasswordCommand :
        AbstractCommand<RecoverPasswordModel, RecoverPasswordResult>
    {
        public ILogger Logger { get; set; } = new NullLogger();
    }
}

namespace PrivateCloud.Application.Users.Queries.GetAllUsers
{
    public partial class GetAllUsersModel
    {
        [Required]
        public System.Security.Claims.ClaimsPrincipal User { get; set; }
    }

   public partial class GetAllUsersResult
    {
        public IEnumerable<User> Users { get; set; }
    }

    public partial class GetAllUsersQuery :
        AbstractQuery<GetAllUsersModel, GetAllUsersResult>
    {
        public ILogger Logger { get; set; } = new NullLogger();
    }
}

namespace PrivateCloud.Application.Users.Queries.GetUser
{
    public partial class GetUserModel
    {
        public Guid Id { get; set; }
        [Required]
        public System.Security.Claims.ClaimsPrincipal User { get; set; }
    }

   public partial class GetUserResult
    {
        public User User { get; set; }
    }

    public partial class GetUserQuery :
        AbstractQuery<GetUserModel, GetUserResult>
    {
        public ILogger Logger { get; set; } = new NullLogger();
    }
}

// Register types
namespace PrivateCloud.Application
{
    public static class ArchitectureConfig
    {
        public static IServiceCollection AddApplicationUseCases(
            this IServiceCollection services)
        {
            services.AddScoped<ICommand<Users.Commands.AuthenticateUser.AuthenticateUserModel, Users.Commands.AuthenticateUser.AuthenticateUserResult>, Users.Commands.AuthenticateUser.AuthenticateUserCommand>();
            services.AddScoped<ICommand<Users.Commands.CreateUser.CreateUserModel, Users.Commands.CreateUser.CreateUserResult>, Users.Commands.CreateUser.CreateUserCommand>();
            services.AddScoped<ICommand<Users.Commands.DeleteUser.DeleteUserModel>, Users.Commands.DeleteUser.DeleteUserCommand>();
            services.AddScoped<ICommand<Users.Commands.ResetPassword.ResetPasswordModel>, Users.Commands.ResetPassword.ResetPasswordCommand>();
            services.AddScoped<ICommand<Users.Commands.RecoverPassword.RecoverPasswordModel, Users.Commands.RecoverPassword.RecoverPasswordResult>, Users.Commands.RecoverPassword.RecoverPasswordCommand>();
            services.AddScoped<IQuery<Users.Queries.GetAllUsers.GetAllUsersModel, Users.Queries.GetAllUsers.GetAllUsersResult>, Users.Queries.GetAllUsers.GetAllUsersQuery>();
            services.AddScoped<IQuery<Users.Queries.GetUser.GetUserModel, Users.Queries.GetUser.GetUserResult>, Users.Queries.GetUser.GetUserQuery>();

            return services;
        }
    }
}


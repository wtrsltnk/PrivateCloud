using PrivateCloud.Application.Interfaces;
using System;

namespace PrivateCloud.Application.Users.Commands.AuthenticateUser
{
    public partial class AuthenticateUserCommand
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenGenerator;

        public AuthenticateUserCommand(
            IUnitOfWork unitOfWork,
            ITokenService tokenGenerator)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _tokenGenerator = tokenGenerator ?? throw new ArgumentNullException(nameof(tokenGenerator));
        }

        protected override AuthenticateUserResult InternalExecute(
            AuthenticateUserModel model)
        {
            var result = _unitOfWork
                .AuthenticateUser(model.Username, model.Password);

            result.Token = _tokenGenerator
                .GenerateToken(result.Id, result.Role);

            return new AuthenticateUserResult()
            {
                AuthenticatedUser = result,
            };
        }
    }
}

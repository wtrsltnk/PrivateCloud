using PrivateCloud.Application.Interfaces;
using PrivateCloud.Domain;
using System;

namespace PrivateCloud.Application.Users.Commands.DeleteUser
{
    public partial class DeleteUserCommand
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteUserCommand(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        protected override void InternalExecute(
            DeleteUserModel model)
        {
            // only allow admins to access other user records
            var currentUserId = Guid.Parse(model.User.Identity.Name);
            if (model.Id != currentUserId && !model.User.IsInRole(Role.Admin))
            {
                throw new InvalidOperationException("non-admin is trying to delete different user");
            }

            var user = _unitOfWork.Users
                .GetById(model.Id);

            _unitOfWork.Users
                .Delete(user);

            _unitOfWork
                .Save();
        }
    }
}

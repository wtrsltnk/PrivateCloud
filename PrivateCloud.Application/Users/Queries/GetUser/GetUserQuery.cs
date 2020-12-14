using PrivateCloud.Application.Interfaces;
using PrivateCloud.Domain;
using System;

namespace PrivateCloud.Application.Users.Queries.GetUser
{
    public partial class GetUserQuery
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetUserQuery(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        protected override GetUserResult InternalExecute(
            GetUserModel model)
        {
            // only allow admins to access other user records
            var currentUserId = Guid.Parse(model.User.Identity.Name);
            if (model.Id != currentUserId && !model.User.IsInRole(Role.Admin))
            {
                throw new InvalidOperationException("non-admin is trying to get info of a different user");
            }

            var result = _unitOfWork
                .Users
                .GetById(model.Id);

            return new GetUserResult()
            {
                User = result,
            };
        }
    }
}

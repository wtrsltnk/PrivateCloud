using PrivateCloud.Application.Interfaces;
using PrivateCloud.Domain;
using System;

namespace PrivateCloud.Application.Users.Queries.GetAllUsers
{
    public partial class GetAllUsersQuery
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllUsersQuery(
            IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        protected override GetAllUsersResult InternalExecute(
            GetAllUsersModel model)
        {
            // only allow admins to access all user records
            if (!model.User.IsInRole(Role.Admin))
            {
                throw new InvalidOperationException("non-admin trying to get all users");
            }

            var result = _unitOfWork
                .Users.GetAll();

            return new GetAllUsersResult()
            {
                Users = result,
            };
        }
    }
}

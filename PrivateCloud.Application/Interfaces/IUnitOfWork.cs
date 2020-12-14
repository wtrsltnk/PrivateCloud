using PrivateCloud.Domain.Users;
using PrivateCloud.Practises.Repositories;
using System;

namespace PrivateCloud.Application.Interfaces
{
    public interface IUnitOfWork :
        IDisposable
    {
        IRepository<User> Users { get; }

        void SetUserPassword(
            Guid id,
            string password);

        User AuthenticateUser(
            string username,
            string password);

        void Save();
    }
}

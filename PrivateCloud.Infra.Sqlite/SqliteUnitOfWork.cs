using PrivateCloud.Application.Interfaces;
using PrivateCloud.Domain.Users;
using PrivateCloud.Infra.Sqlite.Common;
using PrivateCloud.Infra.Sqlite.Dto.Users;
using PrivateCloud.Infra.Sqlite.Repositories;
using PrivateCloud.Practises.Exceptions;
using PrivateCloud.Practises.Repositories;
using System;
using System.Linq;

namespace PrivateCloud.Infra.Sqlite
{
    public class SqliteUnitOfWork :
        IUnitOfWork
    {
        private readonly DataContext _context;
        private readonly Lazy<IRepository<User>> _userRepository;

        public SqliteUnitOfWork(
            DataContext dataContext)
        {
            _context = dataContext ?? throw new ArgumentNullException(nameof(dataContext));

            _userRepository = new Lazy<IRepository<User>>(
                () => new UserRepository(_context, UserMapper, UserMapper));
        }
        public IRepository<User> Users => throw new NotImplementedException();

        public UserMapper UserMapper { get; set; } = new UserMapper();

        public User AuthenticateUser(
            string username,
            string password)
        {
            #region Pre-conditions
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentNullException(nameof(username));
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(password));
            }
            #endregion

            var user = _context.Users.SingleOrDefault(x => x.Username == username);

            if (user == null)
            {
                throw new NotFoundException();
            }

            if (!PasswordUtil.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                // We throw the same exception as above to hide the real reason, no need the make the user any smarter
                throw new NotFoundException();
            }

            return UserMapper.Map(user);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void SetUserPassword(
            Guid id,
            string password)
        {
            var userDto = _context.Users.Find(id);

            if (userDto == null)
            {
                throw new NotFoundException();
            }

            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                PasswordUtil.CreatePasswordHash(password, out passwordHash, out passwordSalt);

                userDto.PasswordHash = passwordHash;
                userDto.PasswordSalt = passwordSalt;
            }

            _context.Users.Update(userDto);

            Save();
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    internalDispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void internalDispose()
        {
            // dont't dispose the context, we dont own that
        }
        #endregion
    }
}

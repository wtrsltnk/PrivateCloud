using PrivateCloud.Domain.Users;
using PrivateCloud.Infra.Sqlite.Dto.Users;
using PrivateCloud.Practises.Mapping;
using System;
using System.Linq;

namespace PrivateCloud.Infra.Sqlite.Repositories
{
    public class UserRepository :
        AbstractRepository<User, UserDto>
    {
        private readonly DataContext _context;

        public UserRepository(
            DataContext dataContext,
            IMapper<User, UserDto> userEntityMapper,
            IMapper<UserDto, User> duserDtoMapper)
            : base(dataContext.Users, userEntityMapper, duserDtoMapper)
        {
            _context = dataContext ?? throw new ArgumentNullException(nameof(dataContext));
        }

        protected override void CheckAdd(
            User entity)
        {
            if (_context.Users.Any(x => x.Username == entity.Username))
            {
                throw new InvalidOperationException($"Username \"{entity.Username}\" is already taken");
            }

            if (_context.Users.Any(x => x.Email == entity.Email))
            {
                throw new InvalidOperationException($"Email \"{entity.Email}\" is already taken");
            }
        }

        protected override void UpdateDto(
            ref UserDto dto,
            User entity)
        {
            dto.Email = entity.Email;
            dto.FirstName = entity.FirstName;
            dto.LastName = entity.LastName;
            dto.Role = entity.Role;
            dto.Username = entity.Username;
        }
    }
}

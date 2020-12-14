using PrivateCloud.Domain.Users;
using PrivateCloud.Practises.Mapping;

namespace PrivateCloud.Infra.Sqlite.Dto.Users
{
    public class UserMapper :
        IMapper<User, UserDto>,
        IMapper<UserDto, User>
    {
        private static OffSync.Mapping.Practises.IMapper<User, UserDto> _userMapper = new OffSync.Mapping.Mappert.Mapper<User, UserDto>(
             b =>
             {
                 b.IgnoreSource(s => s.Token);
                 b.IgnoreTarget(t => t.PasswordHash);
                 b.IgnoreTarget(t => t.PasswordSalt);
                 b.IgnoreTarget(t => t.Flags);
             });

        private static OffSync.Mapping.Practises.IMapper<UserDto, User> _userDtoMapper = new OffSync.Mapping.Mappert.Mapper<UserDto, User>(
             b =>
             {
                 b.IgnoreTarget(s => s.Token);
                 b.IgnoreSource(t => t.PasswordHash);
                 b.IgnoreSource(t => t.PasswordSalt);
                 b.IgnoreSource(t => t.Flags);
             });

        public UserDto Map(
            User source)
        {
            return _userMapper.Map(source);
        }

        public User Map(
            UserDto source)
        {
            return _userDtoMapper.Map(source);
        }
    }
}

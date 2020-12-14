using System;

namespace PrivateCloud.Infra.Sqlite.Dto.Users
{
    public class UserDto :
        IDto
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public string Role { get; set; }

        public int Flags { get; set; }
    }
}

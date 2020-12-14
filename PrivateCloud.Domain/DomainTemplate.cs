using System;
using System.Linq;
using System.Collections.Generic;

namespace PrivateCloud.Domain.Users
{
    public partial class User :
        IEntity
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
    }
}


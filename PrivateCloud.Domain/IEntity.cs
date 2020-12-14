using System;

namespace PrivateCloud.Domain.Users
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
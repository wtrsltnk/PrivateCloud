using System;

namespace PrivateCloud.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(
            Guid userId,
            string userRole);
    }
}

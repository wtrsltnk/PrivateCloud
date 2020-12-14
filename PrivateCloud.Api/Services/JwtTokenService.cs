using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PrivateCloud.Api.Helpers;
using PrivateCloud.Application.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PrivateCloud.Api.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class JwtTokenService :
        ITokenService
    {
        private readonly AppSettings _appSettings;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appSettings"></param>
        public JwtTokenService(
            IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userRole"></param>
        /// <returns></returns>
        public string GenerateToken(
            Guid userId,
            string userRole)
        {
            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userId.ToString()),
                    new Claim(ClaimTypes.Role, userRole)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler
                .CreateToken(tokenDescriptor);

            return tokenHandler
                .WriteToken(token);
        }
    }
}

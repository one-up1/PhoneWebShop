using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PhoneWebShop.Domain.Interfaces;
using PhoneWebShop.Domain.Models.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace PhoneWebShop.Business.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;

        public TokenService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public string Generate(List<Claim> claims)
        {
            if (claims is null || !claims.Any())
                throw new ArgumentNullException(nameof(claims));
            var mySecurityKey = new SymmetricSecurityKey(
            Encoding.ASCII.GetBytes(_jwtSettings.SignKey));
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(
            mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
            });
            return tokenHandler.WriteToken(token);
        }
    }
}

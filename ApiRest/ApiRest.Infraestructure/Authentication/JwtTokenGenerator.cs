using ApiRest.Application.Common.Interfaces.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using ApiRest.Application.Common.Interfaces.Services;
using Microsoft.Extensions.Options;
using ApiRest.Domain.Entities;

namespace ApiRest.Infraestructure.Authentication
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        public readonly IDateTimeProvider _dateTimeProvider;
        public readonly JwtSettings _jwtSettings;
        public JwtTokenGenerator(IDateTimeProvider dateTimeProvider,IOptions< JwtSettings> jwtOptions)
        {
            _dateTimeProvider = dateTimeProvider;
            _jwtSettings = jwtOptions.Value;
        }

        public string GenerateToken(User user)
        {
            //string key = "super-secret-key";
            string key = _jwtSettings.Secret;//no admite largo inferior a 256 bit

            var signingCredencials = new SigningCredentials(
                new SymmetricSecurityKey(
                    key: Encoding.UTF8.GetBytes(key)),
                SecurityAlgorithms.HmacSha256Signature);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.GivenName,user.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName,user.LastName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            };

            var securityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience, 
                expires:_dateTimeProvider.UtcNow.AddMinutes( _jwtSettings.ExpiryMinutes),
                claims:claims,
                signingCredentials:signingCredencials);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}

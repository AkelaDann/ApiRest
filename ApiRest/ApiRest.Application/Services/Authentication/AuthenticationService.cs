using ApiRest.Application.Common.Interfaces.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiRest.Application.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService 
    {
        public readonly IJwtTokenGenerator _jwtTokenGenerator;
        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator) 
        {
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        public AuthenticationResult Register(string firstName, string LastName, string email, string password)
        {
            // usuario existe?

            //crear usuario

            //crear token JWT
            Guid userId = Guid.NewGuid();
            var token = _jwtTokenGenerator.GenerateToken(userId, firstName, LastName);

            return new AuthenticationResult(userId, firstName, LastName, email, token);
        }
        public AuthenticationResult Login(string email, string password)
        {
            return new AuthenticationResult(Guid.NewGuid(), "firstName", "LastName", email, "token");
        }
    }
}

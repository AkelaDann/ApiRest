using ApiRest.Application.Common.Errors;
using ApiRest.Application.Common.Interfaces.Authentication;
using ApiRest.Application.Common.Interfaces.Persistence;
using ApiRest.Domain.Entities;
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
        public readonly IUserRepository _userRepository;
        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }
        public AuthenticationResult Register(string firstName, string lastName, string email, string password)
        {
            // usuario existe?
            if(_userRepository.GetUserByEmail(email) is not null)
            {
                throw new DuplicateEmailException();
            }

            //crear usuario
            var user = new User {
                FirstName = firstName,
                LastName=lastName, 
                Email = email,
                Password = password
            };

            _userRepository.Add(user);
            //crear token JWT
            
            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }
        public AuthenticationResult Login(string email, string password)
        {
            //validar user exist
            if(_userRepository.GetUserByEmail(email) is not User user)
            {
                throw new Exception("User with given email does not exists");
            }
            //validar password 
            if(user.Password != password)
            {
                throw new Exception("Invalid Password.");
            }

            //generar JWT
             var token =_jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user,
                token);
        }
    }
}

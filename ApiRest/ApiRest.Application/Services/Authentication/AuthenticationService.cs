using ApiRest.Application.Common.Errors;
using ApiRest.Application.Common.Interfaces.Authentication;
using ApiRest.Application.Common.Interfaces.Persistence;
using ApiRest.Domain.Common.Errors;
using ApiRest.Domain.Entities;
using ErrorOr;
using FluentResults;

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
        //public oneof <AuthenticationResult,Ierror> Register(string firstName, string lastName, string email, string password)
        //public Result <AuthenticationResult> Register(string firstName, string lastName, string email, string password)
        public ErrorOr <AuthenticationResult> Register(string firstName, string lastName, string email, string password)
        {
            // usuario existe?
            if(_userRepository.GetUserByEmail(email) is not null)
            {
                return Errors.User.DuplicateEmail;
                //return Result.Fail<AuthenticationResult>(new[] { new DuplicateEmailError() });
                //restaurar metodo OneOF
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
        public ErrorOr<AuthenticationResult>Login(string email, string password)
        {
            //validar user exist
            if(_userRepository.GetUserByEmail(email) is not User user)
            {
                return Errors.Authentication.InvalidCredentials;
            }
            //validar password 
            if(user.Password != password)
            {
                return Errors.Authentication.IncorrectPassword;
            }

            //generar JWT
             var token =_jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user,
                token);
        }
    }
}

using ApiRest.Application.Common.Interfaces.Authentication;
using ApiRest.Application.Common.Interfaces.Persistence;
using ApiRest.Application.Services.Authentication.Common;
using ApiRest.Domain.Common.Errors;
using ApiRest.Domain.Entities;
using ErrorOr;

namespace ApiRest.Application.Services.Authentication.Queries

{
    public class AuthenticationQueryService : IAuthenticationQueryService 
    {
        public readonly IJwtTokenGenerator _jwtTokenGenerator;
        public readonly IUserRepository _userRepository;
        public AuthenticationQueryService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
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
                return Errors.Authentication.InvalidCredentials;
            }

            //generar JWT
             var token =_jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(
                user,
                token);
        }
    }
}

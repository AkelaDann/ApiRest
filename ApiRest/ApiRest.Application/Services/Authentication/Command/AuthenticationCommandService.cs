using ApiRest.Application.Common.Interfaces.Authentication;
using ApiRest.Application.Common.Interfaces.Persistence;
using ApiRest.Application.Services.Authentication.Common;
using ApiRest.Domain.Common.Errors;
using ApiRest.Domain.Entities;
using ErrorOr;

namespace ApiRest.Application.Services.Authentication.Command
{
    public class AuthenticationCommandService : IAuthenticationCommandService
    {
        public readonly IJwtTokenGenerator _jwtTokenGenerator;
        public readonly IUserRepository _userRepository;
        public AuthenticationCommandService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }
        //public oneof <AuthenticationResult,Ierror> Register(string firstName, string lastName, string email, string password)
        //public Result <AuthenticationResult> Register(string firstName, string lastName, string email, string password)
        public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
        {
            // usuario existe?
            if (_userRepository.GetUserByEmail(email) is not null)
            {
                return Errors.User.DuplicateEmail;
                //return Result.Fail<AuthenticationResult>(new[] { new DuplicateEmailError() });
                //restaurar metodo OneOF
            }

            //crear usuario
            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password
            };

            _userRepository.Add(user);
            //crear token JWT

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }
    }
}

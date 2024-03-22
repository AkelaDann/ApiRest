using ApiRest.Application.Common.Interfaces.Authentication;
using ApiRest.Application.Common.Interfaces.Persistence;
using ApiRest.Application.Services.Authentication.Common;
using ApiRest.Domain.Common.Errors;
using ApiRest.Domain.Entities;
using ErrorOr;
using MediatR;

namespace ApiRest.Application.Authentication.Command.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
    {
        public readonly IJwtTokenGenerator _jwtTokenGenerator;
        public readonly IUserRepository _userRepository;

        public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }
        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand command, CancellationToken cancellationToken)
        {
            if (_userRepository.GetUserByEmail(command.Email) is not null)
            {
                return Errors.User.DuplicateEmail;
            }

            //crear usuario
            var user = new User
            {
                FirstName = command.FirstName,
                LastName = command.LastName,
                Email = command.Email,
                Password = command.Password
            };

            _userRepository.Add(user);
            //crear token JWT

            var token = _jwtTokenGenerator.GenerateToken(user);

            return new AuthenticationResult(user, token);
        }
    }
}

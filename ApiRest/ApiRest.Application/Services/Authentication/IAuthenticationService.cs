using ErrorOr;
using FluentResults;

namespace ApiRest.Application.Services.Authentication
{
    public interface IAuthenticationService
    {
        //ErrorOr Method
        ErrorOr<AuthenticationResult> Register(string firstName,string LastName,string email,string token);
        //FluentResult Method
        //Result<AuthenticationResult> Register(string firstName,string LastName,string email,string token);
        //OneOF Method
        //OneOf<AuthenticationResult,Ierror> Register(string firstName,string LastName,string email,string token);

        ErrorOr<AuthenticationResult> Login (string email, string password);
    }
}

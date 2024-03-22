using ApiRest.Application.Services.Authentication.Common;
using ErrorOr;

namespace ApiRest.Application.Services.Authentication.Command
{
    public interface IAuthenticationCommandService
    {
        //ErrorOr Method
        ErrorOr<AuthenticationResult> Register(string firstName, string LastName, string email, string token);
        //FluentResult Method
        //Result<AuthenticationResult> Register(string firstName,string LastName,string email,string token);
        //OneOF Method
        //OneOf<AuthenticationResult,Ierror> Register(string firstName,string LastName,string email,string token);

    }
}

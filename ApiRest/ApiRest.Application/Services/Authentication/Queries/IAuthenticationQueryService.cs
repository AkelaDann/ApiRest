using ApiRest.Application.Services.Authentication.Common;
using ErrorOr;

namespace ApiRest.Application.Services.Authentication.Queries
{
    public interface IAuthenticationQueryService
    {

        ErrorOr<AuthenticationResult> Login (string email, string password);
    }
}

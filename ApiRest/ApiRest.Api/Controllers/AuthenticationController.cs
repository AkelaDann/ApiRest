using ApiRest.Api.Filters;
using ApiRest.Application.Common.Errors;
using ApiRest.Application.Services.Authentication;
using ApiRest.Domain.Entities;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace ApiRest.Api.Controllers
{
    
    [Route("auth")]
    [ApiController]
    //[ErrorHandlingFilter] // aplica control de errores por filtro por controlador
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationServices)
        {
                _authenticationService = authenticationServices;
        }

        [HttpPost("register")]
        public ActionResult Register( RegisterRequest request)
        {
            //FluentResults.Result<AuthenticationResult> RegisterResult = _authenticationService.Register(
            ErrorOr<AuthenticationResult>authResult = _authenticationService.Register(
                request.FirstName,
                request.LastName,
                request.Email,
                request.Password);

            
                return authResult.MatchFirst(
                    authResult=> Ok(MapAuthResult( authResult)),
                    firstError=> Problem(statusCode: StatusCodes.Status409Conflict, title: firstError.Description));
           

            //FluentResults Method
            //if (RegisterResult.IsSuccess)
            //{
            //    return Ok(MapAuthResult( RegisterResult.Value));
            //}

            //var firstError = RegisterResult.Errors.First();

            //if (firstError is DuplicateEmailError) 
            //{
            //    return Problem(statusCode: StatusCodes.Status409Conflict, title: "Email aldready exists.");
            //}

            //return Problem();

            //metodo "OneOf" (package oneof) para caputar el flujo del error
            //OneOf<AuthenticationResult, Ierror> RegisterResult = _authenticationService.Register(request.FirstName,request.LastName, request.Email, request.Password);
            //RegisterResult.Match(
            //    authResult => Ok(MapAuthResult(authResult)),
            //    error => Problem(statusCode:(int) error.StatusCode, title: error.ErrorMessage));
            //if (RegisterResult.IsT0)
            //{
            //    var authResult = RegisterResult.AsT0;
            //    AuthenticationResponse response = MapAuthResult(authResult);
            //    return Ok(response);
            //}
            //return Problem(statusCode: StatusCodes.Status409Conflict, title: "Email aldready exists.");

        }
        private static AuthenticationResponse MapAuthResult(AuthenticationResult authResult)
        {
            return new AuthenticationResponse(
                authResult.user.Id,
                authResult.user.FirstName,
                authResult.user.LastName,
                authResult.user.Email,
                authResult.Token);
        }

        [HttpPost("login")]
        public ActionResult Login(LoginRequest request)
        {
            var authResult = _authenticationService.Login( request.Email, request.Password);
            var response = new AuthenticationResponse(
                authResult.user.Id,
                authResult.user.FirstName,
                authResult.user.LastName,
                authResult.user.Email,
                authResult.Token);
            return Ok(response);
        }
    }
}

using ApiRest.Application.Authentication.Command.Register;
using ApiRest.Application.Services.Authentication.Common;
using ApiRest.Domain.Common.Errors;
using ApiRest.Domain.Entities;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ApiRest.Api.Controllers
{

    [Route("auth")]
    
    //[ErrorHandlingFilter] // aplica control de errores por FilterAttribute por controlador
    public class AuthenticationController : ApiController
    {
        #region other inyection services
        //private readonly IAuthenticationCommandService _authenticationCommandService;
        //private readonly IAuthenticationQueryService _authenticationQueryService;
        //public AuthenticationController(IAuthenticationCommandService authenticationCommandService, IAuthenticationQueryService authenticationQueryService)
        //{
        //        _authenticationCommandService = authenticationCommandService;
        //        _authenticationQueryService = authenticationQueryService;
        //}
        #endregion
        private readonly IMediator _mediator;
        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register( RegisterRequest request)
        {

            var command = new RegisterCommand(request.FirstName, request.LastName, request.Email, request.Password);

            ErrorOr<AuthenticationResult> authResult =await _mediator.Send(command);
            
                return authResult.Match(
                    authResult=> Ok(MapAuthResult( authResult)),
                    errors => Problem(errors));
            #region other Methods
            //FluentResults.Result<AuthenticationResult> RegisterResult = _authenticationService.Register(

            //ErrorOr<AuthenticationResult>authResult = _authenticationCommandService.Register(
            //request.FirstName,
            //request.LastName,
            //request.Email,
            //request.Password);

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
            #endregion
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
        public IActionResult Login(LoginRequest request)
        {
            var authResult = _authenticationQueryService.Login( request.Email, request.Password);

            if(authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCredentials)
            {
                return Problem(statusCode: StatusCodes.Status401Unauthorized, title: authResult.FirstError.Description);
            }

            return authResult.Match(
                authResult => Ok(MapAuthResult(authResult)),
                errors => Problem(errors));

            //var response = new AuthenticationResponse(
            //    authResult.user.Id,
            //    authResult.user.FirstName,
            //    authResult.user.LastName,
            //    authResult.user.Email,
            //    authResult.Token);
            //return Ok(response);
        }
    }
}

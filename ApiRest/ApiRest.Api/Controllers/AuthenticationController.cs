using ApiRest.Application.Services.Authentication;
using ApiRest.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ApiRest.Api.Controllers
{
    
    [Route("auth")]
    [ApiController]
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
            var authResult = _authenticationService.Register(request.FirstName,request.LastName, request.Email, request.Password);
            var response = new AuthenticationResponse(authResult.Id,authResult.FirstName,authResult.LastName, authResult.Email,authResult.Token);
            return Ok(response);
        }

        [HttpPost("login")]
        public ActionResult Login(LoginRequest request)
        {
            var authResult = _authenticationService.Login( request.Email, request.Password);
            var response = new AuthenticationResponse(authResult.Id, authResult.FirstName, authResult.LastName, authResult.Email, authResult.Token);
            return Ok(response);
        }
    }
}

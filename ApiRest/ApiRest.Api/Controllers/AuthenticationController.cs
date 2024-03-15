using ApiRest.Application.Services.Authentication;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
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

        [HttpPost("Register")]
        public ActionResult Register( RegisterRequest request)
        {
            //var authResult = _authenticationService.Register( request.Email, request.Password);
            return Ok(request);
        }

        [HttpPost("Login")]
        public ActionResult Login(LoginRequest request)
        {
            
            return Ok(request);
        }
    }
}

using ApiRest.Application.Common.Errors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ApiRest.Api.Controllers
{
    public class ErrorsController : ControllerBase
    {
        [Route("/error")]
        public IActionResult Error() 
        {
            Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

            var (statusCode, message) = exception switch
            {
                IServiceException serviceException => ((int)serviceException.StatusCode,serviceException.ErrorMessage),
                _ => (StatusCodes.Status500InternalServerError, "An unespected error ocurred.")
            };
            return Problem(statusCode:statusCode,title: message);
        }
    }
}

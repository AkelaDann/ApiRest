using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ApiRest.Api.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
                throw;
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError;//500 if unexpected
            var result = JsonSerializer.Serialize("An error ocurred while processing you request");
            context.Response.ContentType = "application/json";
            context.Response.StatusCode=(int)code;
            return context.Response.WriteAsync(result);
        }
    }
}

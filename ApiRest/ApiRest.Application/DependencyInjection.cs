using ApiRest.Application.Services.Authentication;
using ApiRest.Application.Services.Authentication.Command;
using ApiRest.Application.Services.Authentication.Queries;
using Microsoft.Extensions.DependencyInjection;


namespace ApiRest.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationCommandService, AuthenticationCommandService>();
            services.AddScoped<IAuthenticationQueryService, AuthenticationQueryService>();
            return services;
        }
    }
}

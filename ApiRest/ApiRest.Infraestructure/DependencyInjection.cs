using ApiRest.Application.Common.Interfaces.Authentication;
using ApiRest.Infraestructure.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace ApiRest.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraestructure(this IServiceCollection services)
        {
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();           
            return services;
        }
    }
}

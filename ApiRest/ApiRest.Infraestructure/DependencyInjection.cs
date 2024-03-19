using ApiRest.Application.Common.Interfaces.Authentication;
using ApiRest.Application.Common.Interfaces.Persistence;
using ApiRest.Application.Common.Interfaces.Services;
using ApiRest.Infraestructure.Authentication;
using ApiRest.Infraestructure.Persistance;
using ApiRest.Infraestructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace ApiRest.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraestructure(
            this IServiceCollection services, 
            ConfigurationManager configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}

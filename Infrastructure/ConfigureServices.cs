using Application.Common.Interfaces;
using Infrastructure.Authorization;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services) 
        {
            services.AddScoped<IJwtUtils, JwtUtils>();

            services.AddScoped<IAccountService, AccountService>();
            
            return services;
        }

    }
}

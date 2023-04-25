using Application.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services
                .AddMediatR(configuration =>
                {
                    configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                });

            services.AddAutoMapper(Assembly.GetExecutingAssembly());


            return services;
        }
    }
}

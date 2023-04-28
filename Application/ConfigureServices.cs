using Application.Common.Behaviours;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services
                .AddMediatR(configuration =>
                {
                    configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

                    configuration.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

                });

            services.AddAutoMapper(Assembly.GetExecutingAssembly());


            return services;
        }
    }
}

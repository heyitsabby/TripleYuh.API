using Application.Common.Interfaces;
using Infrastructure.Authorization;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration) 
        {
            services
                .AddDbContext<DataContext>(options =>
                    options
                    .UseNpgsql(
                        configuration.GetConnectionString("TripleYuhApiDb"),
                        builder => builder.MigrationsAssembly(typeof(DataContext).Assembly.FullName))
                    .UseSnakeCaseNamingConvention());

            services.AddScoped<IJwtUtils, JwtUtils>();

            services.AddScoped<IAccountService, AccountService>();

            services.AddScoped<IEmailService, EmailService>();

            services.AddScoped<IPostService, PostService>();
            
            return services;
        }

    }
}

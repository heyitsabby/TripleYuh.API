using Application.Common.Interfaces;
using Infrastructure.Authorization;
using Infrastructure.BackgroundJobs;
using Infrastructure.Persistence;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

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

            services.AddScoped<ICommentService, CommentService>();

            services.AddScoped<IVoteService, VoteService>();

            services.AddQuartz(q =>
            {
                // Use a scoped container to create jobs
                q.UseMicrosoftDependencyInjectionJobFactory();

                q.AddJobAndTrigger<UpdatePostsReputationsJob>(configuration);
            });

            services.AddQuartzHostedService(
                   q => q.WaitForJobsToComplete = true);


            return services;
        }

    }
}

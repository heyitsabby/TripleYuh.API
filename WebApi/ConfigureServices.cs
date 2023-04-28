using Infrastructure.Filters;
using System.Text.Json.Serialization;

namespace WebApi
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddWebApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddRouting(options => options.LowercaseUrls = true);

            services
                .AddControllers(options =>
                {
                    options.Filters.Add(new ApiExceptionFilterAttribute());

                    options.SuppressAsyncSuffixInActionNames = false;
                })
                .AddJsonOptions(options =>
                {
                    // serialize enums as strings in api responses (e.g. Role)
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

                    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }
}

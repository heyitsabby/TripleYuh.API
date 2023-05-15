using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence
{
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder();

            var configuration = configurationBuilder
                //.AddJsonFile("../WebApi/appsettings.Local.json", true, true)
                .Build();

            var connectionString = configuration.GetConnectionString("TripleYuhApiDb");

            var dbContextOptionsBuilder = new DbContextOptionsBuilder<DataContext>()
                //.UseNpgsql(connectionString, builder => builder.MigrationsAssembly(typeof(DataContext).Assembly.FullName))
                .UseNpgsql(connectionString)
                .UseSnakeCaseNamingConvention();

            return new DataContext(dbContextOptionsBuilder.Options);
        }
    }
}

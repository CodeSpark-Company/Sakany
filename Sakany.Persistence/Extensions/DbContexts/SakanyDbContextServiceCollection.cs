using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sakany.Persistence.DbContexts;

namespace Sakany.Persistence.Extensions.DbContexts
{
    internal static class SakanyDbContextServiceCollection
    {
        public static IServiceCollection AddSakanyDbContextConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            // Get Connection String From appsettings.json
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // Add Application Db Context
            services.AddDbContext<SakanyDbContext>(options =>
                        options.UseLazyLoadingProxies().UseSqlServer(connectionString, builder =>
                                builder.MigrationsAssembly(typeof(SakanyDbContext).Assembly.FullName)));
            return services;
        }
    }
}
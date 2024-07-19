using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Sakany.Persistence.Extensions.DbContexts
{
    internal static class DbContextsServiceCollection
    {
        public static IServiceCollection AddDbContextsConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSakanyDbContextConfiguration(configuration);
            return services;
        }
    }
}
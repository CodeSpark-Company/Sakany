using Microsoft.Extensions.DependencyInjection;

namespace Sakany.Infrastructure.Extensions
{
    public static class InfrastructureServiceCollection
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services)
        {
            return services;
        }
    }
}
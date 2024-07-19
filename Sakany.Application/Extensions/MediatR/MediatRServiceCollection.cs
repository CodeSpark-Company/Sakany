using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Sakany.Application.Extensions.MediatR
{
    internal static class MediatRServiceCollection
    {
        public static IServiceCollection AddMediatRConfiguration(this IServiceCollection services)
        {
            services.AddMediatR(config => config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            return services;
        }
    }
}
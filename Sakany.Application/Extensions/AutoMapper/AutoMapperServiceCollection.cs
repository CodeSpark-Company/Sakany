using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Sakany.Application.Extensions.AutoMapper
{
    internal static class AutoMapperServiceCollection
    {
        public static IServiceCollection AddAutoMapperConfiguration(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
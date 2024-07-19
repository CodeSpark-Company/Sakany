using Microsoft.Extensions.DependencyInjection;
using Sakany.Application.Interfaces.UnitOfWork;
using UnitOfWorkImplementation = Sakany.Persistence.UnitOfWork.UnitOfWork;

namespace Sakany.Persistence.Extensions.UnitOfWork
{
    public static class UnitOfWorkServiceCollection
    {
        public static IServiceCollection AddUnitOfWorkConfiguration(this IServiceCollection services)
        {
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWorkImplementation));
            return services;
        }
    }
}
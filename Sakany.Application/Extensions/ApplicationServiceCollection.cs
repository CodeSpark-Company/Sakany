using Microsoft.Extensions.DependencyInjection;
using Sakany.Application.Extensions.AutoMapper;
using Sakany.Application.Extensions.MediatR;
using Sakany.Application.Extensions.Validation;

namespace Sakany.Application.Extensions
{
    public static class ApplicationServiceCollection
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            #region Configure AutoMapper

            services.AddAutoMapperConfiguration();

            #endregion Configure AutoMapper

            #region Configure MediatR

            services.AddMediatRConfiguration();

            #endregion Configure MediatR

            #region Configure HttpContextAccessor

            services.AddHttpContextAccessor();

            #endregion Configure HttpContextAccessor

            #region Configure Fluent Validation

            services.AddValidationConfiguration();

            #endregion Configure Fluent Validation

            return services;
        }
    }
}
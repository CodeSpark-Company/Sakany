using Sakany.API.Extensions.Middlewares.Authorization;
using Sakany.API.Extensions.ServiceCollections.ApiVersioning;
using Sakany.API.Extensions.ServiceCollections.Authentication;
using Sakany.API.Extensions.ServiceCollections.Cors;
using Sakany.API.Extensions.ServiceCollections.Exceptions;
using Sakany.API.Extensions.ServiceCollections.Swagger;

namespace Sakany.API.Extensions.ServiceCollections
{
    public static class APIServiceCollection
    {
        public static IServiceCollection AddAPIServiceCollections(this IServiceCollection services, IConfiguration configuration)
        {
            #region Configure Api Versioning

            services.AddApiVersioningConfiguration();

            #endregion Configure Api Versioning

            #region Configure Authentication

            services.AddAuthenticationConfiguration(configuration);

            #endregion Configure Authentication

            #region Configure Authorization

            services.AddAuthorizationConfiguration();

            #endregion Configure Authorization

            #region Configure Controllers

            services.AddControllers();

            #endregion Configure Controllers

            #region Configure Exceptions

            services.AddValidationErrorExceptionConfiguration();

            #endregion Configure Exceptions

            #region Configure Cors

            services.AddCorsConfiguration();

            #endregion Configure Cors

            #region Configure Swagger/OpenAPI

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddSwaggerConfiguration(configuration);

            #endregion Configure Swagger/OpenAPI

            return services;
        }
    }
}
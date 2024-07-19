using Sakany.API.Extensions.ServiceCollections.Swagger;

namespace Sakany.API.Extensions.ServiceCollections
{
    public static class APIServiceCollection
    {
        public static IServiceCollection AddAPIServiceCollections(this IServiceCollection services, IConfiguration configuration)
        {
            #region Configure Swagger/OpenAPI

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddSwaggerConfiguration(configuration);

            #endregion Configure Swagger/OpenAPI

            return services;
        }
    }
}
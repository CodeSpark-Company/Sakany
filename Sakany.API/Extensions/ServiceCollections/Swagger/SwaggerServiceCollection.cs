using Sakany.API.Extensions.ServiceCollections.Swagger.ServiceCollections;

namespace Sakany.API.Extensions.ServiceCollections.Swagger
{
    public static class SwaggerServiceCollection
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEndpointsApiExplorer()
                    .AddSwaggerGenOptions(configuration);

            return services;
        }
    }
}
using Sakany.API.Extensions.ServiceCollections.Swagger.Options;

namespace Sakany.API.Extensions.ServiceCollections.Swagger.ServiceCollections
{
    public static class SwaggerGenServiceCollection
    {
        public static IServiceCollection AddSwaggerGenOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(swagger =>
            {
                swagger.AddSwaggerDocOptions(configuration)
                       .AddSecurityDefinitionOptions()
                       .AddSecurityRequirementOptions();
            });

            return services;
        }
    }
}
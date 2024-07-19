using Microsoft.AspNetCore.Builder;

namespace Sakany.API.Extensions.Middlewares.Swagger
{
    public static class SwaggerApplicationBuilder
    {
        public static IApplicationBuilder UseSwaggerMiddlewares(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("v1/swagger.json", "v1");
            });
            return app;
        }
    }
}
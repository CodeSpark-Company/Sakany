using Sakany.API.Extensions.Middlewares.Swagger;

namespace Sakany.API.Extensions.Middlewares
{
    public static class APIApplicationBuilder
    {
        public static IApplicationBuilder UseAPIMiddlewares(this IApplicationBuilder app)
        {
            app.UseSwaggerMiddlewares();

            return app;
        }
    }
}
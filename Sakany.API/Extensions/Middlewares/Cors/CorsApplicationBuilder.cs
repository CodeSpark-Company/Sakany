namespace Sakany.API.Extensions.Middlewares.Cors
{
    public static class CorsApplicationBuilder
    {
        public static IApplicationBuilder UseCorsMiddlewares(this IApplicationBuilder app)
        {
            app.UseCors("_AllowAll");
            return app;
        }
    }
}
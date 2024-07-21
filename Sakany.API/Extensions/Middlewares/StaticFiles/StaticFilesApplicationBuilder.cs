namespace Sakany.API.Extensions.Middlewares.StaticFiles
{
    public static class StaticFilesApplicationBuilder
    {
        public static IApplicationBuilder UseStaticFilesMiddlewares(this IApplicationBuilder app)
        {
            app.UseStaticFiles();
            return app;
        }
    }
}
using Sakany.Presentation.Middlewares;

namespace Sakany.API.Extensions.Middlewares.Exceptions
{
    public static class GlobalExceptionApplicationBuilder
    {
        public static IApplicationBuilder UseGlobalExceptionMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
            return app;
        }
    }
}
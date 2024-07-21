using Sakany.Presentation.Middlewares;

namespace Sakany.API.Extensions.Middlewares.Exceptions
{
    public static class ExeptionHandlerApplicationBuilder
    {
        public static IApplicationBuilder UseExeptionHandlerMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            return app;
        }
    }
}
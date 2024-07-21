using Sakany.API.Extensions.Middlewares.Authentication;
using Sakany.API.Extensions.Middlewares.Authorization;
using Sakany.API.Extensions.Middlewares.Cors;
using Sakany.API.Extensions.Middlewares.Exceptions;
using Sakany.API.Extensions.Middlewares.StaticFiles;
using Sakany.API.Extensions.Middlewares.Swagger;

namespace Sakany.API.Extensions.Middlewares
{
    public static class APIApplicationBuilder
    {
        public static IApplicationBuilder UseAPIMiddlewares(this IApplicationBuilder app)
        {
            app.UseGlobalExceptionMiddlewares();

            app.UseExeptionHandlerMiddlewares();

            app.UseStatusCodePagesWithReExecute("/Api/V1/Errors/{0}");

            app.UseCorsMiddlewares();

            app.UseSwaggerMiddlewares();

            app.UseStaticFilesMiddlewares();

            app.UseAuthenticationMiddlewares();

            app.UseAuthorizationMiddlewares();

            return app;
        }
    }
}
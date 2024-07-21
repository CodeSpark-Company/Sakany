﻿namespace Sakany.API.Extensions.Middlewares.Authentication
{
    public static class AuthenticationApplicationBuilder
    {
        public static IApplicationBuilder UseAuthenticationMiddlewares(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            return app;
        }
    }
}
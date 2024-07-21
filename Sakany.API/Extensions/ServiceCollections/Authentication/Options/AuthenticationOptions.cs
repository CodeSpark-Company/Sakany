﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Sakany.API.Extensions.ServiceCollections.Authentication.Options
{
    public static class AuthenticationOptions
    {
        public static AuthenticationBuilder AddAuthenticationOptions(this IServiceCollection services)
        {
            return services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });
        }
    }
}
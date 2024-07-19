﻿using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Sakany.API.Extensions.ServiceCollections.Swagger.Options
{
    public static class SecurityRequirementOptions
    {
        public static SwaggerGenOptions AddSecurityRequirementOptions(this SwaggerGenOptions swagger)
        {
            swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme()
                    {
                        Reference = new OpenApiReference()
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme,
                        }
                    },
                    new string[]{}
                }
            });
            return swagger;
        }
    }
}
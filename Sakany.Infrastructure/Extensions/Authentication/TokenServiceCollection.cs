using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Sakany.Application.Interfaces.Services.Authentication;
using Sakany.Infrastructure.Services.Authentication;
using Sakany.Shared.Helpers.JwtConfiguration;

namespace Sakany.Infrastructure.Extensions.Authentication
{
    public static class TokenServiceCollection
    {
        public static IServiceCollection AddTokenService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtConfiguration>(configuration.GetSection("JwtConfiguration"));
            services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<JwtConfiguration>>().Value);

            services.AddScoped(typeof(ITokenService), typeof(TokenService));

            return services;
        }
    }
}
using Microsoft.Extensions.DependencyInjection;
using Sakany.Application.Interfaces.Services.Authentication;
using Sakany.Infrastructure.Services.Authentication;

namespace Sakany.Infrastructure.Extensions.Authentication
{
    public static class AuthenticationServiceCollection
    {
        public static IServiceCollection AddAuthenticationService(this IServiceCollection services)
        {
            services.AddScoped(typeof(IAuthenticationService), typeof(AuthenticationService));
            return services;
        }
    }
}
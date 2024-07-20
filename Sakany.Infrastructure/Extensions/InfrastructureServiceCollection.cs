using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sakany.Infrastructure.Extensions.Authentication;
using Sakany.Infrastructure.Extensions.Mail;
using Sakany.Infrastructure.Extensions.Media;

namespace Sakany.Infrastructure.Extensions
{
    public static class InfrastructureServiceCollection
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTokenService(configuration);
            services.AddAuthenticationService();
            services.AddMediaService();
            services.AddMailService(configuration);

            return services;
        }
    }
}
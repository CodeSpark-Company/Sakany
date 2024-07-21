using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Sakany.Application.Interfaces.Services.Mail;
using Sakany.Infrastructure.Services.Mail;
using Sakany.Shared.Helpers.MailConfiguration;

namespace Sakany.Infrastructure.Extensions.Mail
{
    public static class MailServiceCollection
    {
        public static IServiceCollection AddMailService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MailConfiguration>(configuration.GetSection("MailConfiguration"));
            services.AddSingleton(resolver => resolver.GetRequiredService<IOptions<MailConfiguration>>().Value);

            services.AddScoped(typeof(IMailService), typeof(MailService));
            return services;
        }
    }
}
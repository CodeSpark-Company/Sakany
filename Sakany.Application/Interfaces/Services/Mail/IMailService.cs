using Sakany.Shared.Helpers.MailConfiguration;

namespace Sakany.Application.Interfaces.Services.Mail
{
    public interface IMailService
    {
        Task<bool> SendAsync(MailData mailData);
    }
}
using Notification.Application.Dto;

namespace Notification.Application.Services
{
    public interface IEmailSender
    {
        Task SendEmail(EmailDto notification);
    }
}

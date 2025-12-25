using Notification.Application.Services;
using Notification.Domain.Entities;
using Notification.Domain.Enums;

namespace Notification.Infrastructure.Impl.Services
{
    public class NotificationChannelEmailStrategy(IEmailSender emailSender) : INotificationChannelStrategy
    {
        public NotificationChannel Channel => NotificationChannel.Email;

        public async Task SendAsync(NotificationEntity notification, UserNotificationProfileEntity profile)
        {
            if (!profile.EnableEmail)
                return;

            //TODO: Эмуляция отправки email уведы
            //await emailSender.SendEmail(new EmailDto());
        }
    }
}

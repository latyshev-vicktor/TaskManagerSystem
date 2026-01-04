using Notification.Domain.Entities;
using Notification.Domain.Enums;

namespace Notification.Application.Services
{
    public interface INotificationChannelStrategy
    {
        NotificationChannel Channel { get; }

        Task SendAsync(NotificationEntity notification, UserNotificationProfileEntity profile);
    }
}

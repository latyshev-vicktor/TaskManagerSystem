using Notification.Domain.Entities;

namespace Notification.Application.Services
{
    public interface INotificationSender
    {
        Task Dispatch(NotificationEntity notification, UserNotificationProfileEntity userNotificationProfile);
    }
}

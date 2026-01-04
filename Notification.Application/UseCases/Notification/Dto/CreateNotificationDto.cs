using Notification.Domain.Enums;

namespace Notification.Application.UseCases.Notification.Dto
{
    public record CreateNotificationDto(string Title, string Message, long UserId, NotificationType Type, NotificationChannel[] Channels);
}

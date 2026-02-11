using Notification.Domain.Enums;

namespace Notification.Application.UseCases.Notification.Dto
{
    public record CreateNotificationDto(string Title, string Message, Guid UserId, NotificationType Type, NotificationChannel[] Channels);
}

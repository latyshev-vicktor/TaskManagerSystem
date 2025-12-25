using MediatR;

namespace Notification.Domain.DomainEvents
{
    public record NotificationCreatedEvent(long UserId) : INotification;
}

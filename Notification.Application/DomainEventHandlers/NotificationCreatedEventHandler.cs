using MediatR;
using Notification.DataAccess.Postgres;
using Notification.Domain.DomainEvents;

namespace Notification.Application.DomainEventHandlers
{
    public class NotificationCreatedEventHandler(
        NotificationDbContext dbContext) : INotificationHandler<NotificationCreatedEvent>
    {
        public async Task Handle(NotificationCreatedEvent notification, CancellationToken cancellationToken)
        {
            await Task.FromResult(Task.CompletedTask);
        }
    }
}

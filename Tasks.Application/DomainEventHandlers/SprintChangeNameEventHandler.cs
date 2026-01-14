using MassTransit;
using MediatR;
using TaskManagerSystem.Common.Contracts.Events;
using Tasks.Domain.DomainEvents;

namespace Tasks.Application.DomainEventHandlers
{
    public class SprintChangeNameEventHandler(IPublishEndpoint publishEndpoint) : INotificationHandler<SprintChangeNameEvent>
    {
        public async Task Handle(SprintChangeNameEvent notification, CancellationToken cancellationToken)
        {
            var contractMessage = new UpdatedSprint(notification.NewName, notification.SprintId);
            await publishEndpoint.Publish(contractMessage, cancellationToken);
        }
    }
}

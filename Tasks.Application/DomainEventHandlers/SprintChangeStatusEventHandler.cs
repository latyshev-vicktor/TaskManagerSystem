using MassTransit;
using MediatR;
using TaskManagerSystem.Common.Contracts.Events;
using Tasks.Domain.DomainEvents;

namespace Tasks.Application.DomainEventHandlers
{
    public class SprintChangeStatusEventHandler(IPublishEndpoint publishEndpoint) : INotificationHandler<SprintChangeStatusEvent>
    {
        public async Task Handle(SprintChangeStatusEvent notification, CancellationToken cancellationToken)
        {
            await publishEndpoint.Publish(new SprintChangedStatus
            {
                UserId = notification.UserId,
                SprintName = notification.Name,
                Status = notification.Status.Description,
            }, cancellationToken);
        }
    }
}

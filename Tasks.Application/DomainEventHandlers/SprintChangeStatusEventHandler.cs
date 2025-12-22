using MassTransit;
using MediatR;
using TaskManagerSystem.Common.Contracts;
using Tasks.Domain.DomainEvents;

namespace Tasks.Application.DomainEventHandlers
{
    public class SprintChangeStatusEventHandler(IPublishEndpoint publishEndpoint) : INotificationHandler<SprintChangeStatusEvent>
    {
        public async Task Handle(SprintChangeStatusEvent notification, CancellationToken cancellationToken)
        {
            await publishEndpoint.Publish(new SprintChangeStatus
            {
                Name = notification.Name,
                Status = notification.Status.Description,
                UserId = notification.UserId
            }, cancellationToken);
        }
    }
}

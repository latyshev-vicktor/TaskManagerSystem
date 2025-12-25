using MassTransit;
using MediatR;
using Tasks.Domain.DomainEvents;

namespace Tasks.Application.DomainEventHandlers
{
    public class SprintChangeStatusEventHandler(IPublishEndpoint publishEndpoint) : INotificationHandler<SprintChangeStatusEvent>
    {
        public Task Handle(SprintChangeStatusEvent notification, CancellationToken cancellationToken)
        {
            //TODO: добавить публикацию в шину сообщений
            return Task.CompletedTask;
        }
    }
}

using MediatR;
using Microsoft.AspNetCore.SignalR;
using Tasks.Application.Hubs;
using Tasks.Application.Hubs.Interfaces;
using Tasks.Domain.DomainEvents;

namespace Tasks.Application.DomainEventHandlers
{
    public class SprintChangeStatusEventHandler(IHubContext<SprintHub, ISprintHubClient> hubContext) : INotificationHandler<SprintChangeStatusEvent>
    {
        public async Task Handle(SprintChangeStatusEvent notification, CancellationToken cancellationToken)
        {
            await hubContext.Clients.User(notification.UserId).SprintChangeStatus(notification.Status, cancellationToken);
        }
    }
}

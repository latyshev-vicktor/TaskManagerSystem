using MediatR;
using Tasks.Domain.DomainEvents;

namespace Tasks.Application.DomainEventHandlers
{
    public class DeleteSprintEventHandler : INotificationHandler<DeleteSprintEvent>
    {
        public Task Handle(DeleteSprintEvent notification, CancellationToken cancellationToken)
        {
            //Реализовать отправку уведомления в сервис уведомлений
            return Task.CompletedTask;
        }
    }
}

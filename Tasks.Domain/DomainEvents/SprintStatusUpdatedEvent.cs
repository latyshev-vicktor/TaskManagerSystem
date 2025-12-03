using MediatR;

namespace Tasks.Domain.DomainEvents
{
    public record SprintStatusUpdatedEvent(long Id) : INotification;
}

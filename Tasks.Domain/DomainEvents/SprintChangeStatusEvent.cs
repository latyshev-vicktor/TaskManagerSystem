using MediatR;
using Tasks.Domain.ValueObjects;

namespace Tasks.Domain.DomainEvents
{
    public record SprintChangeStatusEvent(Guid UserId, string Name, SprintStatus Status) : INotification;
}

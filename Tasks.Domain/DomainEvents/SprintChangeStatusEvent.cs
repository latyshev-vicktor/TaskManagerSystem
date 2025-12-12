using MediatR;
using Tasks.Domain.ValueObjects;

namespace Tasks.Domain.DomainEvents
{
    public record SprintChangeStatusEvent(string UserId, SprintStatus Status) : INotification;
}

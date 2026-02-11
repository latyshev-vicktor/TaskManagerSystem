using MediatR;

namespace Tasks.Domain.DomainEvents
{
    public record SprintChangeNameEvent(string NewName, Guid SprintId) : INotification;
}

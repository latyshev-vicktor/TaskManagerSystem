using MediatR;

namespace Tasks.Domain.DomainEvents
{
    public record SprintChangeNameEvent(string NewName, long SprintId) : INotification;
}

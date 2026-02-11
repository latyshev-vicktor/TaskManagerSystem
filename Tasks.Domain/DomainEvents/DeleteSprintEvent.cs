using MediatR;

namespace Tasks.Domain.DomainEvents
{
    public record DeleteSprintEvent(string SprintName, Guid UserId) : INotification;
}

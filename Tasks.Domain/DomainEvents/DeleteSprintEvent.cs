using MediatR;

namespace Tasks.Domain.DomainEvents
{
    public record DeleteSprintEvent(string SprintName, long UserId) : INotification;
}

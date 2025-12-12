using Tasks.Domain.ValueObjects;

namespace Tasks.Application.Hubs.Interfaces
{
    public interface ISprintHubClient
    {
        Task SprintChangeStatus(SprintStatus status, CancellationToken cancellationToken);
    }
}

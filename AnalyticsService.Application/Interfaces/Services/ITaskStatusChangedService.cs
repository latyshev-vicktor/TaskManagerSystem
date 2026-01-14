using TaskManagerSystem.Common.Contracts.Events.Analytics.v1;

namespace AnalyticsService.Application.Interfaces.Services
{
    public interface ITaskStatusChangedService
    {
        Task Handle(TaskStatusChangedEvent contractMessage, CancellationToken ct);
    }
}

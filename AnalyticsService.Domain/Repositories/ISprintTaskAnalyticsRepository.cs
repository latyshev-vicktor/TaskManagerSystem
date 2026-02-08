using AnalyticsService.Domain.Entities.AnalitycsModels;

namespace AnalyticsService.Domain.Repositories
{
    public interface ISprintTaskAnalyticsRepository
    {
        Task<SprintTaskAnalyticsEntity?> GetByTask(long taskId);
        Task<List<SprintTaskAnalyticsEntity>> GetBySprintId(long sprintId);
        Task Add(SprintTaskAnalyticsEntity task);
        Task Save(SprintTaskAnalyticsEntity task);
    }
}

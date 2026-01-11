using AnalyticsService.Domain.Entities.AnalitycsModels;

namespace AnalyticsService.Domain.Repositories
{
    public interface ISprintTaskAnalyticsRepository
    {
        Task<SprintTaskAnalyticsEntity?> GetByTask(Guid taskId);
        Task<List<SprintTaskAnalyticsEntity>> GetBySprintId(Guid sprintId);
        Task Add(SprintTaskAnalyticsEntity task);
        Task Save(SprintTaskAnalyticsEntity task);
    }
}

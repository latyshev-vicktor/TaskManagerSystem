using AnalyticsService.Domain.Entities.AnalitycsModels;

namespace AnalyticsService.Domain.Repositories
{
    public interface ISprintAnalitycsRepository
    {
        Task Add(SprintAnalyticsEntity sprintAnalitycs);
        Task Update(SprintAnalyticsEntity sprintAnalitycs);
        Task<SprintAnalyticsEntity?> GetBySprintId(Guid sprintId);
    }
}

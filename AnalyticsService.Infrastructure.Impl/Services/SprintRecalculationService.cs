using AnalyticsService.Application.Interfaces.Services;
using AnalyticsService.Domain.Enums;
using AnalyticsService.Domain.Repositories;

namespace AnalyticsService.Infrastructure.Impl.Services
{
    public class SprintRecalculationService(
        ISprintAnalitycsRepository sprintAnalitycsRepository,
        ISprintTaskAnalyticsRepository sprintTaskAnalyticsRepository) : ISprintRecalculationService
    {
        public async Task RecalculateSprint(Guid sprintId, Guid userId)
        {
            var tasks = await sprintTaskAnalyticsRepository.GetBySprintId(sprintId);
            var total = tasks.Count;
            var completed = tasks.Count(t => t.Status == TasksStatus.Completed);

            var sprint = await sprintAnalitycsRepository.GetBySprintId(sprintId);
            if(sprint == null)
            {
                throw new ApplicationException($"Не найден спринт с Id {sprintId}");
            }
            
            sprint.Update(total, completed);
            await sprintAnalitycsRepository.Update(sprint);
        }
    }
}

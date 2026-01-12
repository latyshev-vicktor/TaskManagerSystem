using AnalyticsService.Application.Interfaces;
using AnalyticsService.Domain.Entities.AnalitycsModels;
using AnalyticsService.Domain.Enums;
using AnalyticsService.Domain.Repositories;

namespace AnalyticsService.Infrastructure.Impl.Services
{
    public class SprintRecalculationService(
        ISprintAnalitycsRepository sprintAnalitycsRepository,
        ISprintTaskAnalyticsRepository sprintTaskAnalyticsRepository) : ISprintRecalculationService
    {
        public async Task RecalculateSprint(long sprintId, long userId)
        {
            var tasks = await sprintTaskAnalyticsRepository.GetBySprintId(sprintId);
            var total = tasks.Count;
            var completed = tasks.Count(t => t.Status == TasksStatus.Completed);

            var sprint = await sprintAnalitycsRepository.GetBySprintId(sprintId);
            if(sprint == null)
            {
                sprint = new SprintAnalyticsEntity(userId, sprintId);
                await sprintAnalitycsRepository.Add(sprint);
            }
            else
            {
                sprint.Update(total, completed);
                await sprintAnalitycsRepository.Update(sprint);
            }
        }
    }
}

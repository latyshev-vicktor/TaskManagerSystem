using AnalyticsService.DataAccess.Postgres;
using AnalyticsService.Domain.Entities.AnalitycsModels;
using AnalyticsService.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AnalyticsService.Infrastructure.Impl.Repositories
{
    public class SprintTaskAnalyticsRepository(AnalyticsDbContext dbContext) : ISprintTaskAnalyticsRepository
    {
        public async Task Add(SprintTaskAnalyticsEntity task)
        {
            await dbContext.AddAsync(task);
            await dbContext.SaveChangesAsync();
        }

        public Task<List<SprintTaskAnalyticsEntity>> GetBySprintId(long sprintId)
        {
            return dbContext.SprintTaskAnalytics
                .AsNoTracking()
                .Where(x => x.SprintId == sprintId)
                .ToListAsync();
        }

        public async Task<SprintTaskAnalyticsEntity?> GetByTask(long taskId)
        {
            return await dbContext.SprintTaskAnalytics
                .FirstOrDefaultAsync(x => x.TaskId == taskId);
        }

        public async Task Save(SprintTaskAnalyticsEntity task)
        {
            dbContext.Update(task);
            await dbContext.SaveChangesAsync();
        }
    }
}

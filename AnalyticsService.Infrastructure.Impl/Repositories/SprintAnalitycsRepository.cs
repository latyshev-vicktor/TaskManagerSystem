using AnalyticsService.DataAccess.Postgres;
using AnalyticsService.Domain.Entities.AnalitycsModels;
using AnalyticsService.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AnalyticsService.Infrastructure.Impl.Repositories
{
    public class SprintAnalitycsRepository(AnalyticsDbContext dbContext) : ISprintAnalitycsRepository
    {
        public async Task Add(SprintAnalyticsEntity sprintAnalitycs)
        {
            await dbContext.SprintAnalitycs.AddAsync(sprintAnalitycs);
            await dbContext.SaveChangesAsync();
        }

        public async Task<SprintAnalyticsEntity?> GetBySprintId(Guid sprintId)
        {
            return await dbContext.SprintAnalitycs
                .FirstOrDefaultAsync(x => x.SprintId == sprintId);
        }

        public async Task Update(SprintAnalyticsEntity sprintAnalitycs)
        {
            dbContext.Update(sprintAnalitycs);
            await dbContext.SaveChangesAsync();
        }
    }
}

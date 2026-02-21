using AnalyticsService.Application.Interfaces.Services;
using AnalyticsService.DataAccess.Postgres;
using AnalyticsService.Domain.Entities;
using DnsClient.Internal;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskManagerSystem.Common.Contracts.Events;

namespace AnalyticsService.Infrastructure.Impl.Services
{
    public class InsightProcessingService(
        AnalyticsDbContext dbContext,
        IPublishEndpoint publishEndpoint,
        ILogger<InsightProcessingService> logger) : IInsightProcessingService
    {
        public async Task Proccess(IReadOnlyList<InsightEntity> insights)
        {
            try
            {
                var userIds = insights.Select(i => i.UserId).ToHashSet();
                var sprintIds = insights.Select(i => i.SprintId).ToHashSet();
                var types = insights.Select(i => i.Type).ToHashSet();

                var existingInsight = await dbContext.Insights
                    .AsNoTracking()
                    .Where(x => 
                        userIds.Contains(x.UserId) 
                        && sprintIds.Contains(x.SprintId)
                        && types.Contains(x.Type))
                    .Select(x => new { x.UserId, x.SprintId, x.Type })
                    .ToListAsync();

                var existingSet = existingInsight
                    .Select(x => (x.UserId, x.SprintId, x.Type))
                    .ToHashSet();

                var newInsights = insights
                    .Where(x => !existingSet.Contains((x.UserId, x.SprintId, x.Type)))
                    .ToList();

                if (newInsights.Count == 0)
                {
                    return;
                }

                await dbContext.Insights.AddRangeAsync(newInsights);
                await dbContext.SaveChangesAsync();

                var newSprintIds = newInsights
                    .Select(x => x.SprintId)
                    .ToHashSet();

                var sprintNames = await dbContext.SprintAnalitycs
                    .AsNoTracking()
                    .Where(x => newSprintIds.Contains(x.SprintId))
                    .ToDictionaryAsync(x => x.SprintId, x => x.Name);

                await publishEndpoint.PublishBatch(newInsights.Select(x => new InsightEvent(x.Message, sprintNames[x.SprintId], x.UserId)));
            }
            catch(DbUpdateException)
            {
                logger.LogInformation("Дубликат");
                return;
            }
        }
    }
}

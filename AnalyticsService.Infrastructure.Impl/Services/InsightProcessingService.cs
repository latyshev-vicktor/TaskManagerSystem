using AnalyticsService.Application.Interfaces.Services;
using AnalyticsService.DataAccess.Postgres;
using AnalyticsService.Domain.Entities;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Common.Contracts.Events;

namespace AnalyticsService.Infrastructure.Impl.Services
{
    public class InsightProcessingService(
        AnalyticsDbContext dbContext,
        IPublishEndpoint publishEndpoint) : IInsightProcessingService
    {
        public async Task Proccess(IReadOnlyList<InsightEntity> insights)
        {
            foreach(var insight in insights)
            {
                var existInsight = await dbContext.Insights
                    .Where(x => x.UserId == insight.UserId 
                                && x.SprintId == insight.SprintId
                                && x.Type == insight.Type)
                    .AnyAsync();

                if (existInsight)
                {
                    continue;
                }

                await dbContext.Insights.AddAsync(insight);
                await dbContext.SaveChangesAsync();

                var sprintName = await dbContext.SprintAnalitycs.AsNoTracking()
                    .Where(x => x.SprintId == insight.SprintId)
                    .Select(x => x.Name)
                    .FirstOrDefaultAsync();

                var insignEvent = await dbContext.Insights
                    .AsNoTracking()
                    .Where(x => x.Id == insight.Id)
                    .Select(x => new InsightEvent(x.Message, sprintName!, x.UserId))
                    .FirstOrDefaultAsync();

                await publishEndpoint.Publish(insignEvent!);
            }
        }
    }
}

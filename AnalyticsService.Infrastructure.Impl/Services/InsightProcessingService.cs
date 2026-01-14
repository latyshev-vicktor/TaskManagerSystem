using AnalyticsService.Application.Interfaces.Services;
using AnalyticsService.DataAccess.Postgres;
using AnalyticsService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AnalyticsService.Infrastructure.Impl.Services
{
    public class InsightProcessingService(AnalyticsDbContext dbContext) : IInsightProcessingService
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

                //TODO: отправить сформированный инсайт через масстрансит
            }
        }
    }
}

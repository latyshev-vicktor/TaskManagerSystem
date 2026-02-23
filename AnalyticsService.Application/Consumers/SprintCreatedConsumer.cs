using AnalyticsService.DataAccess.Postgres;
using AnalyticsService.Domain.Entities.AnalitycsModels;
using AnalyticsService.Domain.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Common.Contracts.Events.Analytics.v1;

namespace AnalyticsService.Application.Consumers
{
    public class SprintCreatedConsumer(
        ISprintAnalitycsRepository sprintAnalitycsRepository,
        AnalyticsDbContext dbContext) : IConsumer<CreatedNewSprint>
    {
        public async Task Consume(ConsumeContext<CreatedNewSprint> context)
        {
            var contractMessage = context.Message;

            var existEvent = await dbContext
                .MessageConsumers
                .AnyAsync(x => x.EventId == contractMessage.EventId
                            && x.ConsumerName == nameof(SprintCreatedConsumer));

            if (existEvent)
            {
                return;
            }

            var existSprint = await sprintAnalitycsRepository.AnyBySprintId(contractMessage.SprintId);
            if (existSprint)
            {
                return;
            }

            var newSprint = new SprintAnalyticsEntity(contractMessage.UserId, contractMessage.SprintId, contractMessage.Name);
            await sprintAnalitycsRepository.Add(newSprint);
        }
    }
}

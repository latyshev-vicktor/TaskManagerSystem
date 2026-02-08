using AnalyticsService.Domain.Entities.AnalitycsModels;
using AnalyticsService.Domain.Repositories;
using MassTransit;
using TaskManagerSystem.Common.Contracts.Events;

namespace AnalyticsService.Application.Consumers
{
    public class SprintCreatedConsumer(ISprintAnalitycsRepository sprintAnalitycsRepository) : IConsumer<CreatedNewSprint>
    {
        public async Task Consume(ConsumeContext<CreatedNewSprint> context)
        {
            var contractMessage = context.Message;
            var newSprint = new SprintAnalyticsEntity(contractMessage.UserId, contractMessage.SprintId, contractMessage.Name);
            await sprintAnalitycsRepository.Add(newSprint);
        }
    }
}

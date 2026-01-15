using AnalyticsService.Domain.Repositories;
using MassTransit;
using TaskManagerSystem.Common.Contracts.Events;

namespace AnalyticsService.Application.Consumers
{
    public class SprintUpdateNameConsumer(ISprintAnalitycsRepository sprintAnalitycsRepository) : IConsumer<UpdatedSprint>
    {
        public async Task Consume(ConsumeContext<UpdatedSprint> context)
        {
            var contractMessage = context.Message;

            var sprint = await sprintAnalitycsRepository.GetBySprintId(contractMessage.SprintId);
            if(sprint == null)
            {
                throw new ApplicationException($"Не найден спринт с Id {contractMessage.SprintId}");
            }

            sprint.UpdateName(contractMessage.NewName);

            await sprintAnalitycsRepository.Update(sprint);
        }
    }
}

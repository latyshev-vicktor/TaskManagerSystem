using AnalyticsService.Application.UseCases.Sprint.Commands;
using MassTransit;
using MediatR;
using TaskManagerSystem.Common.Contracts.Events;

namespace AnalyticsService.Application.Consumers
{
    public class SprintUpdateNameConsumer(IMediator mediator) : IConsumer<UpdatedSprint>
    {
        public async Task Consume(ConsumeContext<UpdatedSprint> context)
        {
            var contractMessage = context.Message;

            var command = new UpdateSprintNameAnalyticsCommand(contractMessage.SprintId, contractMessage.NewName);
            await mediator.Send(command, context.CancellationToken);
        }
    }
}

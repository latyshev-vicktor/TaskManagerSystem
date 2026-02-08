using MassTransit;
using TaskManagerSystem.Common.Contracts.Events.Analytics.v1;
using AnalyticsService.Application.Interfaces.Services;

namespace AnalyticsService.Application.Consumers
{
    public class TaskStatusChangedConsumer(ITaskStatusChangedService taskStatusChangedService) : IConsumer<TaskStatusChangedEvent>
    {
        public async Task Consume(ConsumeContext<TaskStatusChangedEvent> context)
        {
            var contractMessage = context.Message;
            await taskStatusChangedService.Handle(contractMessage, context.CancellationToken);
        }
    }
}

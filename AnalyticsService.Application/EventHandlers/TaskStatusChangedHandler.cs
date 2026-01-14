using MassTransit;
using TaskManagerSystem.Common.Contracts.Events.Analytics.v1;
using AnalyticsService.Domain.Repositories;
using AnalyticsService.Domain.Enums;
using AnalyticsService.Domain.Entities.AnalitycsModels;
using AnalyticsService.Application.Interfaces.Services;
using AnalyticsService.Application.DetectorPipelines;
using AnalyticsService.Application.Dto;

namespace AnalyticsService.Application.EventHandlers
{
    public class TaskStatusChangedHandler(
        ) : IConsumer<TaskStatusChangedEvent>
    {
        public async Task Consume(ConsumeContext<TaskStatusChangedEvent> context)
        {
            var contractMessage = context.Message;
            
        }
    }
}

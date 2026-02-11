using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Notification.Application.Services;
using Notification.Application.UseCases.Notification.Commands;
using Notification.Application.UseCases.Notification.Dto;
using Notification.DataAccess.Postgres;
using Notification.Domain.Enums;
using Notification.Domain.Specifications;
using TaskManagerSystem.Common.Contracts.Events;

namespace Notification.Application.Consumers
{
    public class SprintChangedStatusConsumer(
        INotificationSender notificationSender, 
        IMediator mediator,
        NotificationDbContext notificationDbContext) : IConsumer<SprintChangedStatus>
    {
        public async Task Consume(ConsumeContext<SprintChangedStatus> context)
        {
            var createNotificationDto = new CreateNotificationDto(
                "Изменение статуса спринта",
                $"Статус спринта {context.Message.SprintName} был изменен на {context.Message.Status}",
                context.Message.UserId,
                NotificationType.SprintStatusChanged,
                [NotificationChannel.SignalR]);

            var notificationResult = await mediator.Send(new CreateNotificationCommand(createNotificationDto));

            if (notificationResult.IsFailure)
                throw new ApplicationException(notificationResult.Error.Message);

            var userProfile = await notificationDbContext
                .UserNotificationProfiles
                .SingleOrDefaultAsync(UserNotificationProfileSpecification.ByUserId(context.Message.UserId));

            await notificationSender.Dispatch(notificationResult.Value, userProfile!);
        }
    }
}

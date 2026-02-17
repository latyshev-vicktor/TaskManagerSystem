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
    public class InsightPublishedConsumer(
        IMediator mediator,
        NotificationDbContext notificationDbContext,
        INotificationSender notificationSender) : IConsumer<InsightEvent>
    {
        public async Task Consume(ConsumeContext<InsightEvent> context)
        {
            var contractMessage = context.Message;
            var notificationDto = new CreateNotificationDto(
                "Предупреждение",
                contractMessage.Message,
                contractMessage.UserId,
                NotificationType.Analytics,
                [NotificationChannel.Email, NotificationChannel.SignalR]);

            var notificationResult = await mediator.Send(new CreateNotificationCommand(notificationDto));

            if (notificationResult.IsFailure)
                throw new ApplicationException(notificationResult.Error.Message);

            var userProfile = await notificationDbContext
                .UserNotificationProfiles
                .FirstOrDefaultAsync(UserNotificationProfileSpecification.ByUserId(context.Message.UserId));

            await notificationSender.Dispatch(notificationResult.Value, userProfile!);
        }
    }
}

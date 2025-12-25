using MassTransit;
using Microsoft.EntityFrameworkCore;
using Notification.Application.Services;
using Notification.DataAccess.Postgres;
using Notification.Domain.Entities;
using Notification.Domain.Enums;
using Notification.Domain.Specifications;
using TaskManagerSystem.Common.Contracts;

namespace Notification.Application.Consumers
{
    public class SprintChangedStatusConsumer(
        INotificationSender notificationSender, 
        NotificationDbContext notificationDbContext) : IConsumer<SprintChangedStatus>
    {
        public async Task Consume(ConsumeContext<SprintChangedStatus> context)
        {
            var notificatioResult = NotificationEntity.Create(
                "Изменение статуса спринта",
                $"Статус спринта {context.Message.SprintName} был изменен на {context.Message.Status}",
                context.Message.UserId,
                NotificationType.SprintStatusChanged,
                [NotificationChannel.SignalR]);

            if (notificatioResult.IsFailure)
                throw new ApplicationException($"Ошибка при создании уведомления об изменении статуса спринта {notificatioResult.Error.Message}");

            await notificationDbContext.Notifications.AddAsync(notificatioResult.Value, context.CancellationToken);
            await notificationDbContext.SaveChangesAsync();

            var userProfile = await notificationDbContext
                .UserNotificationProfiles
                .SingleOrDefaultAsync(UserNotificationProfileSpecification.ByUserId(context.Message.UserId));

            await notificationSender.Dispatch(notificatioResult.Value, userProfile!);
        }
    }
}

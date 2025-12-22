using MassTransit;
using Notification.DataAccess.Postgres;
using Notification.Domain.Entities;
using TaskManagerSystem.Common.Contracts;

namespace Notification.Application.Consumers
{
    public class SprintChangeStatusConsumer(NotificationDbContext notificationDbContext) : IConsumer<SprintChangeStatus>
    {
        public async Task Consume(ConsumeContext<SprintChangeStatus> context)
        {
            var title = "Изменение статуса спринта";
            var message = $"Спринт {context.Message.Name} изменил статус на {context.Message.Status}";
            var notificationResult = NotificationEntity.Create(title, message, context.Message.UserId, Domain.Enums.NotificationType.SprintStatusChanged);
            if(notificationResult.IsFailure)
            {
                throw new ApplicationException($"Ошибка при создании уведомления: {notificationResult.Error.Message}");
            }

            await notificationDbContext.Notifications.AddAsync(notificationResult.Value, context.CancellationToken);
            await notificationDbContext.SaveChangesAsync(context.CancellationToken);
        }
    }
}

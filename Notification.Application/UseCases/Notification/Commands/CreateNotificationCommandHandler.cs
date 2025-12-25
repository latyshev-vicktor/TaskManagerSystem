using MediatR;
using Notification.DataAccess.Postgres;
using Notification.Domain.Entities;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;

namespace Notification.Application.UseCases.Notification.Commands
{
    public class CreateNotificationCommandHandler(NotificationDbContext notificationDbContext) : IRequestHandler<CreateNotificationCommand, IExecutionResult>
    {
        public async Task<IExecutionResult> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            var notificationResult = NotificationEntity.Create(
                request.CreatedDto.Title, 
                request.CreatedDto.Message,
                request.CreatedDto.UserId,
                request.CreatedDto.Type);

            if (notificationResult.IsFailure)
                return ExecutionResult.Failure(notificationResult.Error);

            await notificationDbContext.Notifications.AddAsync(notificationResult.Value, cancellationToken);
            await notificationDbContext.SaveChangesAsync(cancellationToken);

            return ExecutionResult.Success();
        }
    }
}

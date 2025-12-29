using MediatR;
using Notification.DataAccess.Postgres;
using Notification.Domain.Entities;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;

namespace Notification.Application.UseCases.Notification.Commands
{
    public class CreateNotificationCommandHandler(NotificationDbContext dbContext) : IRequestHandler<CreateNotificationCommand, IExecutionResult<NotificationEntity>>
    {
        public async Task<IExecutionResult<NotificationEntity>> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            var newNotificationResult = NotificationEntity.Create(
                request.CreatedDto.Title,
                request.CreatedDto.Message,
                request.CreatedDto.UserId,
                request.CreatedDto.Type,
                request.CreatedDto.Channels);

            if(newNotificationResult.IsFailure)
                return ExecutionResult.Failure<NotificationEntity>(newNotificationResult.Error);

            var newNotification = await dbContext.Notifications.AddAsync(newNotificationResult.Value, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return ExecutionResult.Success(newNotification.Entity);
        }
    }
}

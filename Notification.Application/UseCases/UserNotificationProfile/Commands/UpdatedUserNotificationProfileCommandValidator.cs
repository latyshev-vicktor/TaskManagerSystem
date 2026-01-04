using Microsoft.EntityFrameworkCore;
using Notification.DataAccess.Postgres;
using Notification.Domain.Errors;
using Notification.Domain.Specifications;
using TaskManagerSystem.Common.Errors;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;

namespace Notification.Application.UseCases.UserNotificationProfile.Commands
{
    public class UpdatedUserNotificationProfileCommandValidator(NotificationDbContext dbContext) : RequestValidator<UpdatedUserNotificationProfileCommand>
    {
        public async override Task<IExecutionResult> RequestValidateAsync(UpdatedUserNotificationProfileCommand request, CancellationToken cancellationToken)
        {
            var userNotification = await dbContext
                .UserNotificationProfiles
                .AsNoTracking()
                .FirstOrDefaultAsync(UserNotificationProfileSpecification.ById(request.Dto.Id), cancellationToken);

            if (userNotification == null)
                return ExecutionResult.Failure(BaseEntityError.EntityNotFound("настройка уведомления"));

            if (userNotification.UserId != request.Dto.UserId)
                return ExecutionResult.Failure(UserNotificationProfileError.NotBelongForCurrentUser());

            return ExecutionResult.Success();
        }
    }
}

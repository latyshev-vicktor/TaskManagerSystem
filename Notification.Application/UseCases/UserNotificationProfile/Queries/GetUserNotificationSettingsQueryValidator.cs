using Microsoft.EntityFrameworkCore;
using Notification.DataAccess.Postgres;
using Notification.Domain.Errors;
using Notification.Domain.Specifications;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;

namespace Notification.Application.UseCases.UserNotificationProfile.Queries
{
    public class GetUserNotificationSettingsQueryValidator(NotificationDbContext dbContext) : RequestValidator<GetUserNotificationSettingsQuery>
    {
        public override async Task<IExecutionResult> RequestValidateAsync(GetUserNotificationSettingsQuery request, CancellationToken cancellationToken)
        {
            var existSettings = await dbContext.UserNotificationProfiles
                .AnyAsync(UserNotificationProfileSpecification.ByUserId(request.UserId), cancellationToken);

            if (!existSettings)
                return ExecutionResult.Failure(UserNotificationProfileError.NotFoundByUserId());

            return ExecutionResult.Success();
        }
    }
}

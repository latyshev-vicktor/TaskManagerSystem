using MediatR;
using Microsoft.EntityFrameworkCore;
using Notification.Application.Dto;
using Notification.DataAccess.Postgres;
using Notification.Domain.Specifications;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;

namespace Notification.Application.UseCases.UserNotificationProfile.Queries
{
    public class GetUserNotificationSettingsQueryHandler(NotificationDbContext dbContext) : IRequestHandler<GetUserNotificationSettingsQuery, IExecutionResult<UserNotificationProfileDto>>
    {
        public async Task<IExecutionResult<UserNotificationProfileDto>> Handle(GetUserNotificationSettingsQuery request, CancellationToken cancellationToken)
        {
            var notificationSetting = await dbContext.UserNotificationProfiles
                .Where(UserNotificationProfileSpecification.ByUserId(request.UserId))
                .Select(x => new UserNotificationProfileDto
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    CreatedDate = x.CreatedDate,
                    Email = x.Email,
                    EnableEmail = x.EnableEmail,
                    EnableSignalR = x.EnableSignalR,
                }).FirstOrDefaultAsync(cancellationToken);

            return ExecutionResult.Success(notificationSetting!);
        }
    }
}

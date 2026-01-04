using MediatR;
using Microsoft.EntityFrameworkCore;
using Notification.DataAccess.Postgres;
using Notification.Domain.Specifications;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;

namespace Notification.Application.UseCases.UserNotificationProfile.Commands
{
    public class UpdatedUserNotificationProfileCommandHandler(NotificationDbContext dbContext) : IRequestHandler<UpdatedUserNotificationProfileCommand, IExecutionResult>
    {
        public async Task<IExecutionResult> Handle(UpdatedUserNotificationProfileCommand request, CancellationToken cancellationToken)
        {
            var userNotificationSetting = await dbContext
                .UserNotificationProfiles
                .FirstOrDefaultAsync(UserNotificationProfileSpecification.ById(request.Dto.Id), cancellationToken);

            userNotificationSetting!.SetEnableSignalR(request.Dto.EnableSignalR);
            userNotificationSetting.SetEnableEmail(request.Dto.EnableEmail);

            await dbContext.SaveChangesAsync(cancellationToken);

            return ExecutionResult.Success();
        }
    }
}

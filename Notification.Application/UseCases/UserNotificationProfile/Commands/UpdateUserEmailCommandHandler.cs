using MediatR;
using Microsoft.EntityFrameworkCore;
using Notification.DataAccess.Postgres;
using Notification.Domain.Errors;
using Notification.Domain.Specifications;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;

namespace Notification.Application.UseCases.UserNotificationProfile.Commands
{
    public class UpdateUserEmailCommandHandler(NotificationDbContext dbContext) : IRequestHandler<UpdateUserEmailCommand, IExecutionResult>
    {
        public async Task<IExecutionResult> Handle(UpdateUserEmailCommand request, CancellationToken cancellationToken)
        {
            var userNotificationProfile = await dbContext
                .UserNotificationProfiles
                .FirstOrDefaultAsync(UserNotificationProfileSpecification.ByUserId(request.UserId), cancellationToken);

            if (userNotificationProfile == null)
                return ExecutionResult.Failure(UserNotificationProfileError.NotFoundByUserId());

            userNotificationProfile.SetEmail(request.Email);
            await dbContext.SaveChangesAsync(cancellationToken);

            return ExecutionResult.Success();
        }
    }
}

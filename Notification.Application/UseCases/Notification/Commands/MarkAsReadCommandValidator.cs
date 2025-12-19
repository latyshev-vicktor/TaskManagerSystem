using Microsoft.EntityFrameworkCore;
using Notification.DataAccess.Postgres;
using Notification.Domain.Specifications;
using TaskManagerSystem.Common.Errors;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;

namespace Notification.Application.UseCases.Notification.Commands
{
    public class MarkAsReadCommandValidator(NotificationDbContext notificationDbContext) : RequestValidator<MarkAsReadCommand>
    {
        public override async Task<IExecutionResult> RequestValidateAsync(MarkAsReadCommand request, CancellationToken cancellationToken)
        {
            var existNotification = await notificationDbContext.Notifications.AnyAsync(NotificationSpecification.ById(request.Id), cancellationToken);
            if (existNotification)
            {
                return ExecutionResult.Failure(BaseEntityError.EntityNotFound("уведомление"));
            }

            return ExecutionResult.Success();
        }
    }
}

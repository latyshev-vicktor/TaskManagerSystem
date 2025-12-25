using MediatR;
using Microsoft.EntityFrameworkCore;
using Notification.DataAccess.Postgres;
using Notification.Domain.Specifications;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;

namespace Notification.Application.UseCases.Notification.Commands
{
    internal class MarkAllAsReadCommandHandler(NotificationDbContext notificationDbContext) : IRequestHandler<MarkAllAsReadCommand, IExecutionResult>
    {
        public async Task<IExecutionResult> Handle(MarkAllAsReadCommand request, CancellationToken cancellationToken)
        {
            var notifications = await notificationDbContext.Notifications
                .Where(NotificationSpecification.ByUserId(request.UserId) & NotificationSpecification.IsNotReaded())
                .ToListAsync(cancellationToken);

            foreach(var notification in notifications)
            {
                notification.MarkAsRead();
            }

            await notificationDbContext.SaveChangesAsync(cancellationToken);

            return ExecutionResult.Success();
        }
    }
}

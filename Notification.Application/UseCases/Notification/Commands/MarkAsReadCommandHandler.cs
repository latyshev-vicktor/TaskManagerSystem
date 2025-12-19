using MediatR;
using Microsoft.EntityFrameworkCore;
using Notification.DataAccess.Postgres;
using Notification.Domain.Specifications;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;

namespace Notification.Application.UseCases.Notification.Commands
{
    public class MarkAsReadCommandHandler(NotificationDbContext notificationDbContext) : IRequestHandler<MarkAsReadCommand, IExecutionResult>
    {
        public async Task<IExecutionResult> Handle(MarkAsReadCommand request, CancellationToken cancellationToken)
        {
            var notification = await notificationDbContext
                .Notifications
                .AsNoTracking()
                .Where(NotificationSpecification.ById(request.Id))
                .FirstOrDefaultAsync(cancellationToken);

            var readResult = notification!.MarkAsRead();
            if (readResult.IsFailure)
                return ExecutionResult.Failure(readResult.Error);

            await notificationDbContext.SaveChangesAsync(cancellationToken);
            return ExecutionResult.Success();
        }
    }
}

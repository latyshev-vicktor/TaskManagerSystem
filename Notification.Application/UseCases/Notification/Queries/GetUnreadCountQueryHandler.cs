using MediatR;
using Microsoft.EntityFrameworkCore;
using Notification.DataAccess.Postgres;
using Notification.Domain.Specifications;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;

namespace Notification.Application.UseCases.Notification.Queries
{
    public class GetUnreadCountQueryHandler(NotificationDbContext notificationDbContext) : IRequestHandler<GetUnreadCountQuery, IExecutionResult<long>>
    {
        public async Task<IExecutionResult<long>> Handle(GetUnreadCountQuery request, CancellationToken cancellationToken)
        {
            var result = await notificationDbContext.Notifications
                .AsNoTracking()
                .Where(NotificationSpecification.ByUserId(request.UserId))
                .CountAsync(cancellationToken);

            return ExecutionResult.Success((long)result);
        }
    }
}

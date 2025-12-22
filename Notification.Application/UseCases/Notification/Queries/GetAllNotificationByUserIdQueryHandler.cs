using MediatR;
using Microsoft.EntityFrameworkCore;
using Notification.Application.Dto;
using Notification.DataAccess.Postgres;
using Notification.Domain.Specifications;
using TaskManagerSystem.Common.Interfaces;

namespace Notification.Application.UseCases.Notification.Queries
{
    public class GetAllNotificationByUserIdQueryHandler(NotificationDbContext notificationDbContext) : IRequestHandler<GetAllNotificationByUserIdQuery, IExecutionResult<List<NotificationDto>>>
    {
        public async Task<IExecutionResult<List<NotificationDto>>> Handle(GetAllNotificationByUserIdQuery request, CancellationToken cancellationToken)
        {
            var result = await notificationDbContext.Notifications
                .AsNoTracking()
                .Where(NotificationSpecification.ByUserId(request.UserId))
                .Select(x => new NotificationDto
                {
                    Id = x.Id,
                    CreatedDate = x.CreatedDate,
                    IsRead = x.IsRead,
                    UserId = x.UserId,
                    Message = x.Message,
                    Title = x.Title,
                    ReadDate = x.ReadDate,
                })
                .ToListAsync(cancellationToken);

            return TaskManagerSystem.Common.Implementation.ExecutionResult.Success(result);
        }
    }
}

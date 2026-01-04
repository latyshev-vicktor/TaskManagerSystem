using MediatR;
using Notification.Application.Dto;
using TaskManagerSystem.Common.Interfaces;

namespace Notification.Application.UseCases.Notification.Queries
{
    public record GetAllNotificationByUserIdQuery(long UserId) : IRequest<IExecutionResult<List<NotificationDto>>>;
}

using MediatR;
using Notification.Application.Dto;
using TaskManagerSystem.Common.Interfaces;

namespace Notification.Application.UseCases.UserNotificationProfile.Queries
{
    public record GetUserNotificationSettingsQuery(Guid UserId) : IRequest<IExecutionResult<UserNotificationProfileDto>>;
}

using MediatR;
using Notification.Application.UseCases.UserNotificationProfile.Dto;
using TaskManagerSystem.Common.Interfaces;

namespace Notification.Application.UseCases.UserNotificationProfile.Commands
{
    public record UpdatedUserNotificationProfileCommand(UpdatedUserNotificationProfileDto Dto) : IRequest<IExecutionResult>;
}

using MediatR;
using TaskManagerSystem.Common.Interfaces;

namespace Notification.Application.UseCases.UserNotificationProfile.Commands
{
    public record UpdateUserEmailCommand(long UserId, string Email) : IRequest<IExecutionResult>;
}

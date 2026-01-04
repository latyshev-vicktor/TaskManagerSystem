using MediatR;
using TaskManagerSystem.Common.Interfaces;

namespace Notification.Application.UseCases.UserNotificationProfile.Commands
{
    public record CreateUserNotificationProfileCommand(long UserId, string Email) : IRequest;
}

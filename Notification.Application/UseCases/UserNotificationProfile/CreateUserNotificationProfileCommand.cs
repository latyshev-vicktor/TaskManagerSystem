using MediatR;
using TaskManagerSystem.Common.Interfaces;

namespace Notification.Application.UseCases.UserNotificationProfile
{
    public record CreateUserNotificationProfileCommand(long UserId, string Email) : IRequest;
}

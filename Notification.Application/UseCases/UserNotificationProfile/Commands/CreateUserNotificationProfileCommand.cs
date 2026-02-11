using MediatR;

namespace Notification.Application.UseCases.UserNotificationProfile.Commands
{
    public record CreateUserNotificationProfileCommand(Guid UserId, string Email) : IRequest;
}

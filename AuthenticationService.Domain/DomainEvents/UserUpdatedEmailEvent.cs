using MediatR;

namespace AuthenticationService.Domain.DomainEvents
{
    public record UserUpdatedEmailEvent(long UserId, string Email) : INotification;
}

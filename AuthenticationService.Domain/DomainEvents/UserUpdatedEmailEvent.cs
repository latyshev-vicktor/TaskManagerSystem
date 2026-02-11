using MediatR;

namespace AuthenticationService.Domain.DomainEvents
{
    public record UserUpdatedEmailEvent(Guid UserId, string Email) : INotification;
}

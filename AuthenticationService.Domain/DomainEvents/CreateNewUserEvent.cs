using MediatR;

namespace AuthenticationService.Domain.DomainEvents
{
    public record CreateNewUserEvent(string Email, string FirstName, string LastName) : INotification;
}

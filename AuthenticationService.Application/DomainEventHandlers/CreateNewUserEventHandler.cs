using AuthenticationService.Domain.DomainEvents;
using MassTransit;
using MediatR;
using TaskManagerSystem.Common.Contracts;

namespace AuthenticationService.Application.DomainEventHandlers
{
    public class CreateNewUserEventHandler(IPublishEndpoint publishEndpoint) : INotificationHandler<CreateNewUserEvent>
    {
        public async Task Handle(CreateNewUserEvent notification, CancellationToken cancellationToken)
        {
            await publishEndpoint.Publish(new CreatedNewUser
            {
                Email = notification.Email,
                FirstName = notification.FirstName,
                LastName = notification.LastName,
            }, cancellationToken);
        }
    }
}

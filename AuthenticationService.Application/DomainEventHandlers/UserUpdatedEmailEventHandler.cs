using AuthenticationService.Domain.DomainEvents;
using MassTransit;
using MediatR;
using TaskManagerSystem.Common.Contracts;

namespace AuthenticationService.Application.DomainEventHandlers
{
    public class UserUpdatedEmailEventHandler(IPublishEndpoint publisher) : INotificationHandler<UserUpdatedEmailEvent>
    {
        public async Task Handle(UserUpdatedEmailEvent notification, CancellationToken cancellationToken)
        {
            await publisher.Publish(new UpdatedUserEmailContract
            {
                Email = notification.Email,
                UserId = notification.UserId,
            }, cancellationToken);
        }
    }
}

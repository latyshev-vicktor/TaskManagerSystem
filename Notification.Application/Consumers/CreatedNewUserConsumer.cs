using MassTransit;
using MediatR;
using Notification.Application.UseCases.UserNotificationProfile;
using TaskManagerSystem.Common.Contracts;

namespace Notification.Application.Consumers
{
    public class CreatedNewUserConsumer(IMediator mediator) : IConsumer<CreatedNewUser>
    {
        public async Task Consume(ConsumeContext<CreatedNewUser> context)
        {
            try
            {
                var command = new CreateUserNotificationProfileCommand(context.Message.UserId, context.Message.Email);
                await mediator.Send(command, context.CancellationToken);
            }
            catch (Exception ex)
            {
                await context.Redeliver(TimeSpan.FromMinutes(1));
            }
        }
    }
}

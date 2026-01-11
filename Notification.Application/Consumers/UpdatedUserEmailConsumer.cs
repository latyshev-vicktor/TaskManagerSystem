using MassTransit;
using MediatR;
using Notification.Application.UseCases.UserNotificationProfile.Commands;
using TaskManagerSystem.Common.Contracts.Events;

namespace Notification.Application.Consumers
{
    public class UpdatedUserEmailConsumer(IMediator mediator) : IConsumer<UpdatedUserEmailContract>
    {
        public async Task Consume(ConsumeContext<UpdatedUserEmailContract> context)
        {
            var command = new UpdateUserEmailCommand(context.Message.UserId, context.Message.Email);
            var result = await mediator.Send(command, context.CancellationToken);

            if (result.IsFailure)
                throw new ApplicationException($"Ошибка при обновлении email для настройки уведомлений у пользователя с UserId {context.Message.UserId}: {result.Error.Message}");
        }
    }
}

using MediatR;
using Notification.Application.UseCases.Notification.Dto;
using TaskManagerSystem.Common.Interfaces;

namespace Notification.Application.UseCases.Notification.Commands
{
    public record CreateNotificationCommand(CreateNotificationDto CreatedDto) : IRequest<IExecutionResult>;
}

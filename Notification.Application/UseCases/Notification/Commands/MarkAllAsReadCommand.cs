using MediatR;
using TaskManagerSystem.Common.Interfaces;

namespace Notification.Application.UseCases.Notification.Commands
{
    public record MarkAllAsReadCommand(long UserId) : IRequest<IExecutionResult>;
}

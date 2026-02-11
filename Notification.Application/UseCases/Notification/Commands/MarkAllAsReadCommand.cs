using MediatR;
using TaskManagerSystem.Common.Interfaces;

namespace Notification.Application.UseCases.Notification.Commands
{
    public record MarkAllAsReadCommand(Guid UserId) : IRequest<IExecutionResult>;
}

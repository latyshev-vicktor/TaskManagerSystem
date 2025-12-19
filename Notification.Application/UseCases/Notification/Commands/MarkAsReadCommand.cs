using MediatR;
using TaskManagerSystem.Common.Interfaces;

namespace Notification.Application.UseCases.Notification.Commands
{
    public record MarkAsReadCommand(long Id) : IRequest<IExecutionResult>;
}

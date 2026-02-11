using MediatR;
using TaskManagerSystem.Common.Interfaces;

namespace Tasks.Application.UseCases.Task.Commands
{
    public record DeleteTaskCommand(Guid Id) : IRequest<IExecutionResult>;
}

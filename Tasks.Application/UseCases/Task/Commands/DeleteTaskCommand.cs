using MediatR;
using TaskManagerSystem.Common.Interfaces;

namespace Tasks.Application.UseCases.Task.Commands
{
    public record DeleteTaskCommand(long Id) : IRequest<IExecutionResult>;
}

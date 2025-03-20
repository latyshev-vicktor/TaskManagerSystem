using MediatR;
using TaskManagerSystem.Common.Interfaces;

namespace Tasks.Application.UseCases.Sprint.Commands
{
    public record DeleteSprintCommand(long SprintId) : IRequest<IExecutionResult>;
}

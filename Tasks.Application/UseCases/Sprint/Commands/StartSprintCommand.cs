using MediatR;
using TaskManagerSystem.Common.Interfaces;

namespace Tasks.Application.UseCases.Sprint.Commands
{
    public record StartSprintCommand(long UserId, long SprintId) : IRequest<IExecutionResult>;
}

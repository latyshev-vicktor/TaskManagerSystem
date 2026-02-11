using MediatR;
using TaskManagerSystem.Common.Interfaces;

namespace Tasks.Application.UseCases.Sprint.Commands
{
    public record StartSprintCommand(Guid UserId, Guid SprintId) : IRequest<IExecutionResult>;
}

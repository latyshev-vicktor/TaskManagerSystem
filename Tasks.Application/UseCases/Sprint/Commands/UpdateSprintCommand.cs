using MediatR;
using TaskManagerSystem.Common.Interfaces;

namespace Tasks.Application.UseCases.Sprint.Commands
{
    public record UpdateSprintCommand(long SprintId, string Name, string Description) : IRequest<IExecutionResult<long>>;
}

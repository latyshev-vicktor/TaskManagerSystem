using MediatR;
using TaskManagerSystem.Common.Interfaces;

namespace Tasks.Application.UseCases
{
    public record UpdateSprintCommand(long SprintId, string Name, string Description) : IRequest<IExecutionResult<long>>;
}

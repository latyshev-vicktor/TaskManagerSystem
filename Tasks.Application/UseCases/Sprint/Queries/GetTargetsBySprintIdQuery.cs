using MediatR;
using TaskManagerSystem.Common.Interfaces;
using Tasks.Application.Dto;

namespace Tasks.Application.UseCases.Sprint.Queries
{
    public record GetTargetsBySprintIdQuery(long SprintId) : IRequest<IExecutionResult<List<TargetDto>>>;
}

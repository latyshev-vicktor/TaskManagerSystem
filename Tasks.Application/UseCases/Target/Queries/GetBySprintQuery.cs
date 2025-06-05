using MediatR;
using TaskManagerSystem.Common.Interfaces;
using Tasks.Application.Dto;

namespace Tasks.Application.UseCases.Target.Queries
{
    public record GetBySprintQuery(long SprintId) : IRequest<IExecutionResult<List<TargetDto>>>;
}

using MediatR;
using TaskManagerSystem.Common.Interfaces;
using Tasks.Application.Dto;

namespace Tasks.Application.UseCases.Sprint.Queries
{
    public record GetSprintStatusesQuery : IRequest<IExecutionResult<IReadOnlyList<SprintStatusDto>>>;
}

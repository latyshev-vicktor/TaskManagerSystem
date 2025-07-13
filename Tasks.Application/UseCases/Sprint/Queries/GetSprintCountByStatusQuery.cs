using MediatR;
using TaskManagerSystem.Common.Interfaces;
using Tasks.Application.UseCases.Sprint.Dto;

namespace Tasks.Application.UseCases.Sprint.Queries
{
    public record GetSprintCountByStatusQuery(long UserId) : IRequest<IExecutionResult<SprintCountByStatusDto>>;
}

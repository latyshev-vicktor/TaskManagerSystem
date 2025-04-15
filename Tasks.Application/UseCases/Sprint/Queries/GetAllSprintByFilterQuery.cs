using MediatR;
using TaskManagerSystem.Common.Interfaces;
using Tasks.Application.Dto;
using Tasks.Application.UseCases.Sprint.Dto;

namespace Tasks.Application.UseCases.Sprint.Queries
{
    public record GetAllSprintByFilterQuery(SprintFilter Filter) : IRequest<IExecutionResult<List<SprintDto>>>;
}

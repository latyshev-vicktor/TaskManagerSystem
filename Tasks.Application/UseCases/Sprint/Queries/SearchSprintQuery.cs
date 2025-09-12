using MediatR;
using TaskManagerSystem.Common.Dtos;
using TaskManagerSystem.Common.Interfaces;
using Tasks.Application.Dto;
using Tasks.Application.UseCases.Sprint.Dto;

namespace Tasks.Application.UseCases.Sprint.Queries
{
    public record SearchSprintQuery(SprintFilter Filter) : IRequest<IExecutionResult<SearchData<SprintTableDto>>>;
}

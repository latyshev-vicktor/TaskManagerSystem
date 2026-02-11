using MediatR;
using TaskManagerSystem.Common.Interfaces;
using Tasks.Application.Dto;

namespace Tasks.Application.UseCases.SprintWeek.Queries
{
    public record GetWeeksBySprintIdQuery(Guid SprintId) : IRequest<IExecutionResult<List<SprintWeekDto>>>;
}

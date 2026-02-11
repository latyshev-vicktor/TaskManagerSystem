using MediatR;
using TaskManagerSystem.Common.Interfaces;
using Tasks.Application.Dto;

namespace Tasks.Application.UseCases.FIeldActivity.Queires
{
    public record GetFieldActivitiesBySprintQuery(Guid UserId, Guid SprintId) : IRequest<IExecutionResult<List<FieldActivityForSprintDto>>>;
}

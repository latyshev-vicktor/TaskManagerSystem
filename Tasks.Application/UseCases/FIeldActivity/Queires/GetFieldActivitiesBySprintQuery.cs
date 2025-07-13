using MediatR;
using TaskManagerSystem.Common.Interfaces;
using Tasks.Application.Dto;

namespace Tasks.Application.UseCases.FIeldActivity.Queires
{
    public record GetFieldActivitiesBySprintQuery(long UserId, long SprintId) : IRequest<IExecutionResult<List<FieldActivityForSprintDto>>>;
}

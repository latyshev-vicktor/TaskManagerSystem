using MediatR;
using TaskManagerSystem.Common.Interfaces;
using Tasks.Application.Dto;

namespace Tasks.Application.UseCases.FIeldActivity.Queires
{
    public record GetMyFieldActivitiesQuery(Guid UserId) : IRequest<IExecutionResult<List<FieldActivityDto>>>;
}

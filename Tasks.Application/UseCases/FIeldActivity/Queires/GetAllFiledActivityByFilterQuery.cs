using MediatR;
using TaskManagerSystem.Common.Interfaces;
using Tasks.Application.Dto;
using Tasks.Application.UseCases.FIeldActivity.Dto;

namespace Tasks.Application.UseCases.FIeldActivity.Queires
{
    public record GetAllFiledActivityByFilterQuery(FieldActivityFilter Filter) : IRequest<IExecutionResult<List<FieldActivityDto>>>;
}

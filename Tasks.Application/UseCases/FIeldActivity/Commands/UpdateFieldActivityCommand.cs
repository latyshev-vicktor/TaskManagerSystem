using MediatR;
using TaskManagerSystem.Common.Interfaces;
using Tasks.Application.Dto;

namespace Tasks.Application.UseCases.FIeldActivity.Commands
{
    public record UpdateFieldActivityCommand(FieldActivityDto Dto) : IRequest<IExecutionResult<Guid>>;
}

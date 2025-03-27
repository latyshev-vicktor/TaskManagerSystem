using MediatR;
using TaskManagerSystem.Common.Interfaces;
using Tasks.Application.UseCases.Sprint.Dto;

namespace Tasks.Application.UseCases.Sprint.Commands
{
    public record CreateSprintCommand(CreateSprintDto Dto, long UserId) : IRequest<IExecutionResult<long>>;
}

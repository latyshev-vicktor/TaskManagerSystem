using MediatR;
using TaskManagerSystem.Common.Interfaces;
using Tasks.Application.Dto;

namespace Tasks.Application.UseCases.Sprint.Commands
{
    public record UpdateSprintCommand(SprintDto Dto) : IRequest<IExecutionResult<long>>;
}

using MediatR;
using TaskManagerSystem.Common.Interfaces;
using Tasks.Application.Dto;

namespace Tasks.Application.UseCases.Target.Commands
{
    public record UpdateTargetCommand(TargetDto Dto) : IRequest<IExecutionResult<long>>;
}

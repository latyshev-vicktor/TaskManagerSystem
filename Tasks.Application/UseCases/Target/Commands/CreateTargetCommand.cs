using MediatR;
using TaskManagerSystem.Common.Interfaces;
using Tasks.Application.UseCases.Target.Dto;

namespace Tasks.Application.UseCases.Target.Commands
{
    public record CreateTargetCommand(CreateTargetDto Dto) : IRequest<IExecutionResult<long>>;
}

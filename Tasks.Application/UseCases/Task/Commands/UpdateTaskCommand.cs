using MediatR;
using TaskManagerSystem.Common.Interfaces;
using Tasks.Application.Dto;

namespace Tasks.Application.UseCases.Task.Commands
{
    public record UpdateTaskCommand(TaskDto Dto) : IRequest<IExecutionResult<long>>;
}

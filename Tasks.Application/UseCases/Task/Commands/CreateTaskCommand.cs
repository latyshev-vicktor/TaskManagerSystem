using MediatR;
using TaskManagerSystem.Common.Interfaces;
using Tasks.Application.UseCases.Task.Dto;

namespace Tasks.Application.UseCases.Task.Commands
{
    public record CreateTaskCommand(CreateTaskDto CreateDto) : IRequest<IExecutionResult<long>>;
}

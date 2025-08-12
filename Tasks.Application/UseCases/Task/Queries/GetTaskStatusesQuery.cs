using MediatR;
using TaskManagerSystem.Common.Interfaces;
using Tasks.Application.Dto;

namespace Tasks.Application.UseCases.Task.Queries
{
    public record GetTaskStatusesQuery : IRequest<IExecutionResult<IReadOnlyList<TaskStatusDto>>>;
}

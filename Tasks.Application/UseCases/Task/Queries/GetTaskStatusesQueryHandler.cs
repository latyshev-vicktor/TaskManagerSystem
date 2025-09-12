using MediatR;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.Application.Dto;
using Tasks.Domain.ValueObjects;

namespace Tasks.Application.UseCases.Task.Queries
{
    public class GetTaskStatusesQueryHandler : IRequestHandler<GetTaskStatusesQuery, IExecutionResult<IReadOnlyList<TaskStatusDto>>>
    {
        public Task<IExecutionResult<IReadOnlyList<TaskStatusDto>>> Handle(GetTaskStatusesQuery request, CancellationToken cancellationToken)
        {
            var result = TasksStatus.All.Select(status => new TaskStatusDto
            {
                Name = status.Value,
                Description = status.Description
            }).ToList();

            return System.Threading.Tasks.Task.FromResult<IExecutionResult<IReadOnlyList<TaskStatusDto>>>(ExecutionResult.Success<IReadOnlyList<TaskStatusDto>>(result));
        }
    }
}

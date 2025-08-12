using MediatR;
using TaskManagerSystem.Common.Interfaces;
using Tasks.Application.Dto;

namespace Tasks.Application.UseCases.Task.Queries
{
    public class GetTaskStatusesQueryHandler : IRequestHandler<GetTaskStatusesQuery, IExecutionResult<IReadOnlyList<TaskStatusDto>>>
    {
        public Task<IExecutionResult<IReadOnlyList<TaskStatusDto>>> Handle(GetTaskStatusesQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

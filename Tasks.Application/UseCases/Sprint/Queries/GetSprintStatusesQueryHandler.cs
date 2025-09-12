using CSharpFunctionalExtensions;
using MediatR;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.Application.Dto;
using Tasks.Domain.ValueObjects;

namespace Tasks.Application.UseCases.Sprint.Queries
{
    public class GetSprintStatusesQueryHandler : IRequestHandler<GetSprintStatusesQuery, IExecutionResult<IReadOnlyList<SprintStatusDto>>>
    {
        public Task<IExecutionResult<IReadOnlyList<SprintStatusDto>>> Handle(GetSprintStatusesQuery request, CancellationToken cancellationToken)
        {
            var result = SprintStatus.All.Select(x => new SprintStatusDto
            {
                Name = x.Value,
                Description = x.Description
            }).ToList();

            return System.Threading.Tasks.Task.FromResult(ExecutionResult.Success<IReadOnlyList<SprintStatusDto>>(result));
        }
    }
}

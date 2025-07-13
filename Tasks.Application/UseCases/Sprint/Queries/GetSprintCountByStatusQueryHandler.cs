using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.Application.UseCases.Sprint.Dto;
using Tasks.DataAccess.Postgres;
using Tasks.Domain.Specifications;
using Tasks.Domain.ValueObjects;

namespace Tasks.Application.UseCases.Sprint.Queries
{
    public class GetSprintCountByStatusQueryHandler(TaskDbContext dbContext) : IRequestHandler<GetSprintCountByStatusQuery, IExecutionResult<SprintCountByStatusDto>>
    {
        public async Task<IExecutionResult<SprintCountByStatusDto>> Handle(GetSprintCountByStatusQuery request, CancellationToken cancellationToken)
        {
            var data = await dbContext.Sprints
                                        .AsNoTracking()
                                        .Where(SprintSpecification.ByUserId(request.UserId))
                                        .GroupBy(x => x.Status.Value)
                                        .ToDictionaryAsync(x => x.Key, y => y.Count(), cancellationToken);

            var created = data.TryGetValue(SprintStatus.Created.Value, out int createdCount) ? createdCount : 0;
            var inProgress = data.TryGetValue(SprintStatus.InProgress.Value, out int inProgressCount) ? inProgressCount : 0;
            var completed = data.TryGetValue(SprintStatus.Completed.Value, out int completedCount) ? completedCount : 0;

            return ExecutionResult.Success(new SprintCountByStatusDto(created, inProgress, completed));
        }
    }
}

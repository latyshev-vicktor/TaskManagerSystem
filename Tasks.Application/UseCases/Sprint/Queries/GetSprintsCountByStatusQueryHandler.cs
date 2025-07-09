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
    class GetSprintsCountByStatusQueryHandler(TaskDbContext dbContext) : IRequestHandler<GetSprintsCountByStatusQuery, IExecutionResult<SprintCountByStatusDto>>
    {
        public async Task<IExecutionResult<SprintCountByStatusDto>> Handle(GetSprintsCountByStatusQuery request, CancellationToken cancellationToken)
        {
            var dict = await dbContext.Sprints
                                      .AsNoTracking()
                                      .Where(SprintSpecification.ByUserId(request.UserId))
                                      .GroupBy(g => g.Status.Value)
                                      .Select(x => new { Status = x.Key, Count = x.Count() } )
                                      .ToDictionaryAsync(x => x.Status, x => x.Count, cancellationToken);

            return ExecutionResult.Success(new SprintCountByStatusDto
            {
                Created = dict.GetValueOrDefault(SprintStatus.Created.Value, 0),
                InProgress = dict.GetValueOrDefault(SprintStatus.InProgress.Value, 0),
                Completed = dict.GetValueOrDefault(SprintStatus.Completed.Value, 0),
            });
        }
    }
}

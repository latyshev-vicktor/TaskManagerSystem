using MediatR;
using Microsoft.EntityFrameworkCore;
using NSpecifications;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.Application.Dto;
using Tasks.Application.Mappings;
using Tasks.DataAccess.Postgres;
using Tasks.Domain.Errors;
using Tasks.Domain.Specifications;

namespace Tasks.Application.UseCases.FIeldActivity.Queires
{
    public class GetFieldActivitiesBySprintQueryHandler(TaskDbContext dbContext) : IRequestHandler<GetFieldActivitiesBySprintQuery, IExecutionResult<List<FieldActivityForSprintDto>>>
    {
        public async Task<IExecutionResult<List<FieldActivityForSprintDto>>> Handle(GetFieldActivitiesBySprintQuery request, CancellationToken cancellationToken)
        {
            var currentUserSprints = await dbContext.Sprints
                                                    .AsNoTracking()
                                                    .Where(x => x.UserId == request.UserId)
                                                    .AnyAsync(cancellationToken);

            if (currentUserSprints == false)
                return ExecutionResult.Failure<List<FieldActivityForSprintDto>>(SprintError.SprintDoesNotBelongForCurrentUser());

            var spec = SprintFieldActivitySpecification.BySprintId(request.SprintId);

            var data = await dbContext.SprintFieldActivities
                                      .AsNoTracking()
                                      .Where(spec)
                                      .Select(x => new FieldActivityForSprintDto
                                      {
                                          Id = x.Id,
                                          CreatedDate = x.FieldActivity!.CreatedDate,
                                          Name = x.FieldActivity!.Name,
                                          Targets = x.Targets.Select(t => new TargetDto
                                          {
                                              Id = t.Id,
                                              Name = t.Name.Name,
                                              CreatedDate = t.CreatedDate,
                                              SprintFieldActivityId = t.SprintFieldActivityId,
                                              Tasks = t.Tasks.Select(task => new TaskDto
                                              {
                                                  Id = task.Id,
                                                  Name = task.Name.Name,
                                                  Description = task.Description.Description,
                                                  CreatedDate = task.CreatedDate,
                                                  TargetId = task.TargetId,
                                                  Status = new TaskStatusDto
                                                  {
                                                      Name = task.Status.Value,
                                                      Description = task.Status.Description,
                                                  },
                                              }).ToList()
                                          }).ToList(),
                                      })
                                      .ToListAsync(cancellationToken);

            return ExecutionResult.Success(data);
        }
    }
}

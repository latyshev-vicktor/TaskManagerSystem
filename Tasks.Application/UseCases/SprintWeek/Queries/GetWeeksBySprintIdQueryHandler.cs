using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.Application.Dto;
using Tasks.DataAccess.Postgres;
using Tasks.Domain.Specifications;

namespace Tasks.Application.UseCases.SprintWeek.Queries
{
    public class GetWeeksBySprintIdQueryHandler(TaskDbContext taskDbContext) : IRequestHandler<GetWeeksBySprintIdQuery, IExecutionResult<List<SprintWeekDto>>>
    {
        public async Task<IExecutionResult<List<SprintWeekDto>>> Handle(GetWeeksBySprintIdQuery request, CancellationToken cancellationToken)
        {
            var result = await taskDbContext.SprintWeeks
                .AsNoTracking()
                .Where(SprintWeekSpecification.BySprintId(request.SprintId))
                .Select(x => new SprintWeekDto
                {
                    Id = x.Id,
                    WeekNumber = x.WeekNumber,
                    SprintId = x.SprintId,
                    CreatedDate = x.CreatedDate,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    Tasks = x.Tasks.Select(x => new TaskDto
                    {
                        Id = x.Id,
                        CreatedDate = x.CreatedDate,
                        Description = x.Description.Description,
                        Name = x.Name.Name,
                        Status = new TaskStatusDto
                        {
                            Name = x.Status.Value,
                            Description = x.Status.Description
                        }
                    }).ToList()
                }).ToListAsync(cancellationToken);


            return ExecutionResult.Success(result);
        }
    }
}

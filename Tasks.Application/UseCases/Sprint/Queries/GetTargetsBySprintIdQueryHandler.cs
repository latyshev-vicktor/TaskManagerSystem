using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.Application.Dto;
using Tasks.DataAccess.Postgres;
using Tasks.Domain.Specifications;

namespace Tasks.Application.UseCases.Sprint.Queries
{
    public class GetTargetsBySprintIdQueryHandler(TaskDbContext taskDbContext) : IRequestHandler<GetTargetsBySprintIdQuery, IExecutionResult<List<TargetDto>>>
    {
        public async Task<IExecutionResult<List<TargetDto>>> Handle(GetTargetsBySprintIdQuery request, CancellationToken cancellationToken)
        {
            var targets = await taskDbContext.Targets
                .AsNoTracking()
                .Where(TargetSpecification.BySprintId(request.SprintId))
                .Select(entity => new TargetDto
                {
                    Id = entity.Id,
                    CreatedDate = entity.CreatedDate,
                    SprintId = entity.SprintId,
                    Name = entity.Name.Name,
                    Tasks = entity.Tasks.OrderByDescending(x => x.CreatedDate).Select(task => new TaskDto
                    {
                        Id = task.Id,
                        CreatedDate = task.CreatedDate,
                        Name = task.Name.Name,
                        Description = task.Description.Description,
                        Status = new TaskStatusDto
                        {
                            Name = task.Status.Value,
                            Description = task.Status.Description
                        },
                        TargetId = task.TargetId
                    }).ToList()
                }).ToListAsync(cancellationToken);

            return ExecutionResult.Success(targets);
        }
    }
}

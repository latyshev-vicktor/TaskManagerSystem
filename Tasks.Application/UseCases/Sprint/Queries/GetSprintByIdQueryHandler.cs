using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.Application.Dto;
using Tasks.Application.Mappings;
using Tasks.DataAccess.Postgres;
using Tasks.Domain.Specifications;

namespace Tasks.Application.UseCases.Sprint.Queries
{
    public class GetSprintByIdQueryHandler(TaskDbContext dbContext) : IRequestHandler<GetSprintByIdQuery, IExecutionResult<SprintDto>>
    {
        public async Task<IExecutionResult<SprintDto>> Handle(GetSprintByIdQuery request, CancellationToken cancellationToken)
        {
            var sprint = await dbContext.Sprints
                                        .AsNoTracking()
                                        .Where(SprintSpecification.ById(request.Id))
                                        .Select(entity => new SprintDto
                                        {
                                            Id = entity.Id,
                                            UserId = entity.UserId,
                                            CreatedDate = entity.CreatedDate,
                                            Name = entity.Name.Name,
                                            Description = entity.Description.Description,
                                            SprintStatus = new SprintStatusDto
                                            {
                                                Name = entity.Status.Value,
                                                Description = entity.Status.Description,
                                            },
                                            FieldActivities = entity.SprintFieldActivities.Select(fa => new FieldActivityForSprintDto
                                            {
                                                Id = fa.FieldActivity.Id,
                                                CreatedDate = fa.FieldActivity.CreatedDate,
                                                Name = fa.FieldActivity.Name,
                                                UserId = fa.FieldActivity.UserId,
                                                SprintId = fa.SprintId
                                            }).ToList(),
                                            StartDate = entity.StartDate,
                                            EndDate = entity.EndDate
                                        })
                                        .FirstOrDefaultAsync(cancellationToken);

            return ExecutionResult.Success(sprint!);
        }
    }
}

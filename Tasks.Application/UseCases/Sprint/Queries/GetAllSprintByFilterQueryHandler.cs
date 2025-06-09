using MediatR;
using Microsoft.EntityFrameworkCore;
using NSpecifications;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.Application.Dto;
using Tasks.Application.Mappings;
using Tasks.DataAccess.Postgres;
using Tasks.Domain.Entities;
using Tasks.Domain.Specifications;
using Tasks.Domain.ValueObjects;

namespace Tasks.Application.UseCases.Sprint.Queries
{
    public class GetAllSprintByFilterQueryHandler(TaskDbContext dbContext) : IRequestHandler<GetAllSprintByFilterQuery, IExecutionResult<List<SprintDto>>>
    {
        public async Task<IExecutionResult<List<SprintDto>>> Handle(GetAllSprintByFilterQuery request, CancellationToken cancellationToken)
        {
            var spec = Spec.Any<SprintEntity>();

            var filter = request.Filter;

            if (filter.Id.HasValue)
                spec &= SprintSpecification.ById(filter.Id.Value);

            if (filter.EndDate.HasValue)
                spec &= SprintSpecification.LessDateEnd(filter.EndDate.Value);

            if (filter.StartDate.HasValue)
                spec &= SprintSpecification.ModeStartEnd(filter.StartDate.Value);

            if (filter.UserId.HasValue)
                spec &= SprintSpecification.ByUserId(filter.UserId.Value);

            if (!string.IsNullOrEmpty(filter.Name))
                spec &= SprintSpecification.ByName(filter.Name);

            if (!string.IsNullOrEmpty(filter.Status))
                spec &= SprintSpecification.ByStatus(filter.Status);

            if (!string.IsNullOrEmpty(filter.Description))
                spec &= SprintSpecification.ByDescription(filter.Description);

            if (filter.FieldActivityIds != null)
                spec &= SprintSpecification.ByFieldActivities(filter.FieldActivityIds);

            var result = await dbContext.Sprints.AsNoTracking()
                                                .Where(spec)
                                                .Select(x => x.ToDto())
                                                .ToListAsync(cancellationToken);

            return ExecutionResult.Success(result);
        }
    }
}

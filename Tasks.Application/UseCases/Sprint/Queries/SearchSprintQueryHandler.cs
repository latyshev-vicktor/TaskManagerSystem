using MediatR;
using Microsoft.EntityFrameworkCore;
using NSpecifications;
using TaskManagerSystem.Common.Dtos;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.Application.Dto;
using Tasks.Application.Mappings;
using Tasks.DataAccess.Postgres;
using Tasks.Domain.Entities;
using Tasks.Domain.Specifications;

namespace Tasks.Application.UseCases.Sprint.Queries
{
    internal class SearchSprintQueryHandler(TaskDbContext dbContext) : IRequestHandler<SearchSprintQuery, IExecutionResult<SearchData<SprintDto>>>
    {
        public async Task<IExecutionResult<SearchData<SprintDto>>> Handle(SearchSprintQuery request, CancellationToken cancellationToken)
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

            if (filter.FieldActivityId != null)
                spec &= SprintSpecification.ByFieldActivityId(filter.FieldActivityId.Value);

            if (filter.FieldActivityIds != null)
                spec &= SprintSpecification.ByFieldActivities(filter.FieldActivityIds);

            var dbQuery = dbContext.Sprints
                                   .AsNoTracking()
                                   .Where(spec);

            if (!string.IsNullOrWhiteSpace(filter.SortBy))
            {
                switch(filter.SortBy)
                {
                    case nameof(SprintEntity.CreatedDate):
                        dbQuery = filter.SortDesc ? dbQuery.OrderByDescending(x => x.CreatedDate) : dbQuery.OrderBy(x => x.CreatedDate);
                        break;
                }
            }

            var count = await dbQuery.CountAsync(cancellationToken);

            var data = await dbQuery.Skip(filter.Skip)
                                    .Take(filter.Take)
                                    .Select(x => x.ToDto())
                                    .ToListAsync(cancellationToken);

            var result = new SearchData<SprintDto>(data, count);

            return ExecutionResult.Success(result);
        }
    }
}

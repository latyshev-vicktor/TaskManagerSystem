using MediatR;
using Microsoft.EntityFrameworkCore;
using NSpecifications;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.Application.Dto;
using Tasks.DataAccess.Postgres;
using Tasks.Domain.Entities;
using Tasks.Domain.Specifications;

namespace Tasks.Application.UseCases.FIeldActivity.Queires
{
    public class GetAllFiledActivityByFilterQueryHandler(TaskDbContext dbContext) : IRequestHandler<GetAllFiledActivityByFilterQuery, IExecutionResult<List<FieldActivityDto>>>
    {
        public async Task<IExecutionResult<List<FieldActivityDto>>> Handle(GetAllFiledActivityByFilterQuery request, CancellationToken cancellationToken)
        {
            var spec = Spec.Any<FieldActivityEntity>();

            if (request.Filter.Id != null)
                spec &= FieldActivitySpecification.ById(request.Filter.Id.Value);

            if (request.Filter.UserId != null)
                spec &= FieldActivitySpecification.ByUserId(request.Filter.UserId.Value);

            if (!string.IsNullOrWhiteSpace(request.Filter.Name))
                spec &= FieldActivitySpecification.ByName(request.Filter.Name);

            var fieldActivities = await dbContext.FieldActivities
                                                 .AsNoTracking()
                                                 .Where(spec)
                                                 .Select(x => new FieldActivityDto
                                                 {
                                                     Id = x.Id,
                                                     UserId = x.UserId,
                                                     Name = x.Name,
                                                     CreatedDate = x.CreatedDate
                                                 }).ToListAsync(cancellationToken);

            return ExecutionResult.Success(fieldActivities);
        }
    }
}

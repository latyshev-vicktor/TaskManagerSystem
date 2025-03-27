using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.Application.Dto;
using Tasks.DataAccess.Postgres;
using Tasks.Domain.Specifications;

namespace Tasks.Application.UseCases.FIeldActivity.Queires
{
    public class GetMyFieldActivitiesQueryHandler(TaskDbContext dbContext) : IRequestHandler<GetMyFieldActivitiesQuery, IExecutionResult<List<FieldActivityDto>>>
    {
        public async Task<IExecutionResult<List<FieldActivityDto>>> Handle(GetMyFieldActivitiesQuery request, CancellationToken cancellationToken)
        {
            var result = await dbContext.FieldActivities
                                        .Where(FieldActivitySpecification.ByUserId(request.UserId))
                                        .Select(x => new FieldActivityDto
                                        {
                                            Id = x.Id,
                                            UserId = x.UserId,
                                            Name = x.Name,
                                            CreatedDate = x.CreatedDate
                                        })
                                        .ToListAsync(cancellationToken);

            return ExecutionResult.Success(result);
        }
    }
}

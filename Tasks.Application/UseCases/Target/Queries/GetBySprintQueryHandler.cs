using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.Application.Dto;
using Tasks.Application.Mappings;
using Tasks.DataAccess.Postgres;
using Tasks.Domain.Specifications;

namespace Tasks.Application.UseCases.Target.Queries
{
    public class GetBySprintQueryHandler(TaskDbContext dbContext) : IRequestHandler<GetBySprintQuery, IExecutionResult<List<TargetDto>>>
    {
        public async Task<IExecutionResult<List<TargetDto>>> Handle(GetBySprintQuery request, CancellationToken cancellationToken)
        {
            var result = await dbContext.Targets
                                        .AsNoTracking()
                                        .AsSplitQuery()
                                            .Include(x => x.Tasks)
                                        .Where(TargetSpecification.BySprintId(request.SprintId))
                                        .Select(x => x.ToDto())
                                        .ToListAsync(cancellationToken);

            return ExecutionResult.Success(result);
        }
    }
}

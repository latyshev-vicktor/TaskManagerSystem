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
    public class GetTargetsBySprintIdQueryHandler(TaskDbContext taskDbContext) : IRequestHandler<GetTargetsBySprintIdQuery, IExecutionResult<List<TargetDto>>>
    {
        public async Task<IExecutionResult<List<TargetDto>>> Handle(GetTargetsBySprintIdQuery request, CancellationToken cancellationToken)
        {
            var targets = await taskDbContext.Targets
                .AsNoTracking()
                    .Include(x => x.Tasks)
                .Where(TargetSpecification.BySprintId(request.SprintId))
                .Select(x => x.ToDto())
                .ToListAsync(cancellationToken);

            return ExecutionResult.Success(targets);
        }
    }
}

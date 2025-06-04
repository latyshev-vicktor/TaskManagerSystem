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
                                            .Include(x => x.Targets)
                                        .Where(SprintSpecification.ById(request.Id))
                                        .Select(x => x.ToDto())
                                        .FirstOrDefaultAsync(cancellationToken);

            return ExecutionResult.Success(sprint!);
        }
    }
}

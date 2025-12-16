using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.DataAccess.Postgres;
using Tasks.Domain.Specifications;

namespace Tasks.Application.UseCases.Sprint.Commands
{
    public class StartSprintCommandHandler(TaskDbContext dbContext) : IRequestHandler<StartSprintCommand, IExecutionResult>
    {
        public async Task<IExecutionResult> Handle(StartSprintCommand request, CancellationToken cancellationToken)
        {
            var sprint = await dbContext.Sprints
                                        .Include(x => x.SprintWeeks)
                                        .FirstOrDefaultAsync(SprintSpecification.ById(request.SprintId), cancellationToken);

            var result = sprint!.StartSprint();
            if (result.IsFailure)
                return ExecutionResult.Failure(result.Error);

            if (sprint.CreatedDate.Date != DateTimeOffset.UtcNow.Date)
            {
                sprint.RecalculationStartAndEndDateWeek();
            }

            await dbContext.SaveChangesAsync(cancellationToken);

            return ExecutionResult.Success();
        }
    }
}

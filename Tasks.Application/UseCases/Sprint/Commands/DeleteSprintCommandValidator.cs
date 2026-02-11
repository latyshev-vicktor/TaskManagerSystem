using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Common.Errors;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.DataAccess.Postgres;
using Tasks.Domain.Errors;
using Tasks.Domain.Specifications;
using Tasks.Domain.ValueObjects;

namespace Tasks.Application.UseCases.Sprint.Commands
{
    public class DeleteSprintValidator(TaskDbContext dbContext) : RequestValidator<DeleteSprintCommand>
    {
        public async override Task<IExecutionResult> RequestValidateAsync(DeleteSprintCommand request, CancellationToken cancellationToken)
        {
            var deletedSprint = await dbContext.Sprints.FirstOrDefaultAsync(SprintSpecification.ById(request.SprintId), cancellationToken);
            if (deletedSprint == null)
                return ExecutionResult.Failure(BaseEntityError.EntityNotFound("спринт"));

            if (deletedSprint.Status == SprintStatus.Completed)
                return ExecutionResult.Failure(SprintError.CompletedSprintNotBeDeleted());

            return ExecutionResult.Success();
        }
    }
}

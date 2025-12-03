using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Common.Errors;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.DataAccess.Postgres;
using Tasks.Domain.Specifications;

namespace Tasks.Application.UseCases.Task.Commands
{
    public class CompleteTaskCommandValidator(TaskDbContext dbContext) : RequestValidator<CompleteTaskCommand>
    {
        public async override Task<IExecutionResult> RequestValidateAsync(CompleteTaskCommand request, CancellationToken cancellationToken)
        {
            var existTask = await dbContext.Tasks
                .AsNoTracking()
                .AnyAsync(TaskSpecification.ById(request.Id), cancellationToken);

            if (!existTask)
                return ExecutionResult.Failure(BaseEntityError.EntityNotFound("задача"));

            return ExecutionResult.Success();
        }
    }
}

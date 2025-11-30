using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.DataAccess.Postgres;
using Tasks.Domain.Specifications;

namespace Tasks.Application.UseCases.Task.Commands
{
    public class CompleteTaskCommandHandler(TaskDbContext dbContext) : IRequestHandler<CompleteTaskCommand, IExecutionResult>
    {
        public async Task<IExecutionResult> Handle(CompleteTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await dbContext.Tasks.FirstOrDefaultAsync(TaskSpecification.ById(request.Id), cancellationToken);

            var completeTaskResult = task!.Completed();
            if (completeTaskResult.IsFailure)
                return ExecutionResult.Failure(completeTaskResult.Error);

            await dbContext.SaveChangesAsync(cancellationToken);

            return ExecutionResult.Success();
        }
    }
}

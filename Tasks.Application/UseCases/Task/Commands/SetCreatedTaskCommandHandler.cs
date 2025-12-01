using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.DataAccess.Postgres;
using Tasks.Domain.Specifications;

namespace Tasks.Application.UseCases.Task.Commands
{
    public class SetCreatedTaskCommandHandler(TaskDbContext dbContext) : IRequestHandler<SetCreatedTaskCommand, IExecutionResult>
    {
        public async Task<IExecutionResult> Handle(SetCreatedTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await dbContext.Tasks.FirstOrDefaultAsync(TaskSpecification.ById(request.Id), cancellationToken);

            var createdTaskResult = task!.SetCreatedStatus();
            if (createdTaskResult.IsFailure)
                return ExecutionResult.Failure(createdTaskResult.Error);

            await dbContext.SaveChangesAsync(cancellationToken);

            return ExecutionResult.Success();
        }
    }
}

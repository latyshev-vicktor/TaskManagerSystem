using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.DataAccess.Postgres;
using Tasks.Domain.Specifications;

namespace Tasks.Application.UseCases.Task.Commands
{
    public class DeleteTaskCommandHandler(TaskDbContext dbContext) : IRequestHandler<DeleteTaskCommand, IExecutionResult>
    {
        public async Task<IExecutionResult> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await dbContext.Tasks
                .FirstOrDefaultAsync(TaskSpecification.ById(request.Id), cancellationToken);

            task!.Delete();

            await dbContext.SaveChangesAsync(cancellationToken);

            return ExecutionResult.Success();
        }
    }
}

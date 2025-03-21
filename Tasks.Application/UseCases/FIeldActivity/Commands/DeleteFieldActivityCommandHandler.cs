using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.DataAccess.Postgres;
using Tasks.Domain.Specifications;

namespace Tasks.Application.UseCases.FIeldActivity.Commands
{
    public class DeleteFieldActivityCommandHandler(TaskDbContext dbContext) : IRequestHandler<DeleteFieldActivityCommand, IExecutionResult>
    {
        public async Task<IExecutionResult> Handle(DeleteFieldActivityCommand request, CancellationToken cancellationToken)
        {
            var fieldActivity = await dbContext.FieldActivities.FirstOrDefaultAsync(FieldActivitySpecification.ById(request.Id), cancellationToken);
            fieldActivity!.Delete();

            await dbContext.SaveChangesAsync(cancellationToken);

            return ExecutionResult.Success();
        }
    }
}

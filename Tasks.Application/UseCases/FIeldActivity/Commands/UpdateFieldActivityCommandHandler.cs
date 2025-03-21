using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.DataAccess.Postgres;
using Tasks.Domain.Specifications;

namespace Tasks.Application.UseCases.FIeldActivity.Commands
{
    public class UpdateFieldActivityCommandHandler(TaskDbContext dbContext) : IRequestHandler<UpdateFieldActivityCommand, IExecutionResult<long>>
    {
        public async Task<IExecutionResult<long>> Handle(UpdateFieldActivityCommand request, CancellationToken cancellationToken)
        {
            var fieldActivity = await dbContext.FieldActivities.FirstOrDefaultAsync(FieldActivitySpecification.ById(request.Dto.Id), cancellationToken);
            fieldActivity!.SetName(request.Dto.Name);

            await dbContext.SaveChangesAsync(cancellationToken);

            return ExecutionResult.Success(fieldActivity.Id);
        }
    }
}

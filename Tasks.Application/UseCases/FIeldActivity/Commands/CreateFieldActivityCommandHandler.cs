using MediatR;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.DataAccess.Postgres;
using Tasks.Domain.Entities;

namespace Tasks.Application.UseCases.FIeldActivity.Commands
{
    public class CreateFieldActivityCommandHandler(TaskDbContext dbContext) : IRequestHandler<CreateFieldActivityCommand, IExecutionResult<long>>
    {
        public async Task<IExecutionResult<long>> Handle(CreateFieldActivityCommand request, CancellationToken cancellationToken)
        {
            var newFieldActivity = new FieldActivityEntity(request.Name, request.UserId);
            var savedFieldActivity = await dbContext.AddAsync(newFieldActivity, cancellationToken);

            await dbContext.SaveChangesAsync(cancellationToken);

            return ExecutionResult.Success(savedFieldActivity.Entity.Id);
        }
    }
}

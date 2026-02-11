using MediatR;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.DataAccess.Postgres;
using Tasks.Domain.Entities;

namespace Tasks.Application.UseCases.FIeldActivity.Commands
{
    public class CreateFieldActivityCommandHandler(TaskDbContext dbContext) : IRequestHandler<CreateFieldActivityCommand, IExecutionResult<Guid>>
    {
        public async Task<IExecutionResult<Guid>> Handle(CreateFieldActivityCommand request, CancellationToken cancellationToken)
        {
            var newFieldActivity = new FieldActivityEntity(request.Name, request.UserId);
            await dbContext.AddAsync(newFieldActivity, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return ExecutionResult.Success(newFieldActivity.Id);
        }
    }
}

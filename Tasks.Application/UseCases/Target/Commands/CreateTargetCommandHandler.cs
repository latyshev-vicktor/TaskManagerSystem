using MediatR;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.DataAccess.Postgres;
using Tasks.Domain.Entities;

namespace Tasks.Application.UseCases.Target.Commands
{
    public class CreateTargetCommandHandler(TaskDbContext dbContext) : IRequestHandler<CreateTargetCommand, IExecutionResult<Guid>>
    {
        public async Task<IExecutionResult<Guid>> Handle(CreateTargetCommand request, CancellationToken cancellationToken)
        {
            var newTarget = TargetEntity.Create(request.Dto.Name, request.Dto.SprintId);

            await dbContext.AddAsync(newTarget.Value, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return ExecutionResult.Success(newTarget.Value.Id);
        }
    }
}

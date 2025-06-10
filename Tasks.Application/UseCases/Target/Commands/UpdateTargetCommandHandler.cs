using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.DataAccess.Postgres;
using Tasks.Domain.Specifications;

namespace Tasks.Application.UseCases.Target.Commands
{
    public class UpdateTargetCommandHandler(TaskDbContext dbContext) : IRequestHandler<UpdateTargetCommand, IExecutionResult<long>>
    {
        public async Task<IExecutionResult<long>> Handle(UpdateTargetCommand request, CancellationToken cancellationToken)
        {
            var target = await dbContext.Targets
                                        .Where(TargetSpecification.ById(request.Dto.Id))
                                        .FirstOrDefaultAsync(cancellationToken);

            target!.SetName(request.Dto.Name);
            target.SetSprintFieldActivity(request.Dto.SprintFieldActivityId);

            await dbContext.SaveChangesAsync(cancellationToken);

            return ExecutionResult.Success(target.Id);
        }
    }
}

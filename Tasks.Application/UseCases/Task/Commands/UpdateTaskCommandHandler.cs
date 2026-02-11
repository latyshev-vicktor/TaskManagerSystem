using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.DataAccess.Postgres;
using Tasks.Domain.Specifications;

namespace Tasks.Application.UseCases.Task.Commands
{
    public class UpdateTaskCommandHandler(TaskDbContext dbContext) : IRequestHandler<UpdateTaskCommand, IExecutionResult<Guid>>
    {
        public async Task<IExecutionResult<Guid>> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await dbContext.Tasks
                .Where(TaskSpecification.ById(request.Dto.Id))
                .FirstOrDefaultAsync(cancellationToken);

            task!.SetDescription(request.Dto.Description);
            task.SetName(request.Dto.Name);

            await dbContext.SaveChangesAsync(cancellationToken);

            return ExecutionResult.Success(task.Id);
        }
    }
}

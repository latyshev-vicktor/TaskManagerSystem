using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.DataAccess.Postgres;

namespace Tasks.Application.UseCases.Sprint.Commands
{
    public class UpdateSprintCommandHandler(TaskDbContext dbContext) : IRequestHandler<UpdateSprintCommand, IExecutionResult<long>>
    {
        public async Task<IExecutionResult<long>> Handle(UpdateSprintCommand request, CancellationToken cancellationToken)
        {
            var task = await dbContext.Tasks.FirstOrDefaultAsync(x => x.Id == request.Dto.Id, cancellationToken);
            task!.SetDescription(request.Dto.Description);
            task!.SetName(request.Dto.Name);

            await dbContext.SaveChangesAsync(cancellationToken);

            return ExecutionResult.Success(task.Id);
        }
    }
}

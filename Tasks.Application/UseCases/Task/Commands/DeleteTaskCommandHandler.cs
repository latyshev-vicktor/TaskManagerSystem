using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Common.Contracts.Events.Analytics.v1;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.Application.Services;
using Tasks.DataAccess.Postgres;
using Tasks.Domain.Specifications;

namespace Tasks.Application.UseCases.Task.Commands
{
    public class DeleteTaskCommandHandler(TaskDbContext dbContext, IOutboxMessageService outboxMessageService) : IRequestHandler<DeleteTaskCommand, IExecutionResult>
    {
        public async Task<IExecutionResult> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await dbContext.Tasks
                .FirstOrDefaultAsync(TaskSpecification.ById(request.Id), cancellationToken);

            task!.Delete();

            var linkagesSprintId = await dbContext
                .Sprints
                .AsNoTracking()
                .Where(SprintSpecification.ByTaskId(task.Id))
                .Select(x => x.Id)
                .FirstOrDefaultAsync(cancellationToken);

            await outboxMessageService.Add(new DeleteTaskEvent(Guid.NewGuid(), DateTime.UtcNow, task.Id, linkagesSprintId));

            await dbContext.SaveChangesAsync(cancellationToken);

            return ExecutionResult.Success();
        }
    }
}

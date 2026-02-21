using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Common.Contracts.Events.Analytics.v1;
using TaskManagerSystem.Common.Interfaces;
using Tasks.Application.Services;
using Tasks.DataAccess.Postgres;
using Tasks.Domain.Specifications;
using ExecutionResult = TaskManagerSystem.Common.Implementation.ExecutionResult;

namespace Tasks.Application.UseCases.Task.Commands
{
    public class SetCreatedTaskCommandHandler(TaskDbContext dbContext, IOutboxMessageService outboxMessageService) : IRequestHandler<SetCreatedTaskCommand, IExecutionResult>
    {
        public async Task<IExecutionResult> Handle(SetCreatedTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await dbContext.Tasks.FirstOrDefaultAsync(TaskSpecification.ById(request.Id), cancellationToken);

            var createdTaskResult = task!.SetCreatedStatus();
            if (createdTaskResult.IsFailure)
                return ExecutionResult.Failure(createdTaskResult.Error);

            var linkagesSprintInfo = await dbContext.Sprints
                .AsNoTracking()
                .Where(SprintSpecification.ByTaskId(task.Id))
                .Select(x => new { x.Id, x.UserId })
                .FirstAsync(cancellationToken);

            await outboxMessageService.Add(new TaskStatusChangedEvent(
                Guid.NewGuid(),
                DateTimeOffset.UtcNow,
                task.Id,
                linkagesSprintInfo.Id,
                linkagesSprintInfo.UserId,
                task.Status.Value.ToString()));

            await dbContext.SaveChangesAsync(cancellationToken);

            return ExecutionResult.Success();
        }
    }
}

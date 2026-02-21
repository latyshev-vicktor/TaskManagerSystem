using MassTransit;
using MassTransit.Testing;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Common.Contracts.Events.Analytics.v1;
using TaskManagerSystem.Common.Interfaces;
using Tasks.Application.Services;
using Tasks.DataAccess.Postgres;
using Tasks.Domain.Specifications;
using Tasks.Domain.ValueObjects;
using ExecutionResult = TaskManagerSystem.Common.Implementation.ExecutionResult;

namespace Tasks.Application.UseCases.Task.Commands
{
    public class CompleteTaskCommandHandler(TaskDbContext dbContext, IOutboxMessageService outboxMessageService) : IRequestHandler<CompleteTaskCommand, IExecutionResult>
    {
        public async Task<IExecutionResult> Handle(CompleteTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await dbContext.Tasks.FirstOrDefaultAsync(TaskSpecification.ById(request.Id), cancellationToken);

            var completeTaskResult = task!.Completed();
            if (completeTaskResult.IsFailure)
                return ExecutionResult.Failure(completeTaskResult.Error);

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
                TasksStatus.Completed.Value.ToString()));

            await dbContext.SaveChangesAsync(cancellationToken);

            return ExecutionResult.Success();
        }
    }
}

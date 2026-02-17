using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Common.Contracts.Events.Analytics.v1;
using TaskManagerSystem.Common.Interfaces;
using Tasks.Application.Services;
using Tasks.DataAccess.Postgres;
using Tasks.Domain.Entities;
using Tasks.Domain.Specifications;
using Tasks.Domain.ValueObjects;
using ExecutionResult = TaskManagerSystem.Common.Implementation.ExecutionResult;

namespace Tasks.Application.UseCases.Task.Commands
{
    public class CreateTaskCommandHandler(TaskDbContext dbContext, IOutboxMessageService outboxMessageService) : IRequestHandler<CreateTaskCommand, IExecutionResult<Guid>>
    {
        public async Task<IExecutionResult<Guid>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var newTask = TaskEntity.Create(request.CreateDto.Name, request.CreateDto.Description, request.CreateDto.TargetId, request.CreateDto.WeekId);
            await dbContext.Tasks.AddAsync(newTask.Value, cancellationToken);

            var linkagesSprintInfo = await dbContext.Sprints
                .AsNoTracking()
                .Where(SprintSpecification.ByTargetId(request.CreateDto.TargetId))
                .Select(x => new 
                {
                    x.Id,
                    x.UserId
                })
                .FirstOrDefaultAsync(cancellationToken);

            var taskStatusChangedEvent = new TaskStatusChangedEvent(
                Guid.NewGuid(),
                DateTimeOffset.UtcNow,
                newTask.Value.Id,
                linkagesSprintInfo!.Id, 
                linkagesSprintInfo.UserId, 
                TasksStatus.Created.Value.ToString());

            await outboxMessageService.Add(taskStatusChangedEvent);

            await dbContext.SaveChangesAsync(cancellationToken);

            return ExecutionResult.Success(newTask.Value.Id);
        }
    }
}

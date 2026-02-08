using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Common.Contracts.Events.Analytics.v1;
using TaskManagerSystem.Common.Interfaces;
using Tasks.DataAccess.Postgres;
using Tasks.Domain.Entities;
using Tasks.Domain.Specifications;
using Tasks.Domain.ValueObjects;
using ExecutionResult = TaskManagerSystem.Common.Implementation.ExecutionResult;

namespace Tasks.Application.UseCases.Task.Commands
{
    public class CreateTaskCommandHandler(TaskDbContext dbContext, IPublishEndpoint publishEndpoint) : IRequestHandler<CreateTaskCommand, IExecutionResult<long>>
    {
        public async Task<IExecutionResult<long>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var newTask = TaskEntity.Create(request.CreateDto.Name, request.CreateDto.Description, request.CreateDto.TargetId, request.CreateDto.WeekId);

            var taskResult = await dbContext.Tasks.AddAsync(newTask.Value, cancellationToken);

            await dbContext.SaveChangesAsync(cancellationToken);

            var linkagesSprintInfo = await dbContext.Sprints
                .AsNoTracking()
                .Where(SprintSpecification.ByTaskId(taskResult.Entity.Id))
                .Select(x => new 
                {
                    x.Id,
                    x.UserId
                })
                .FirstOrDefaultAsync(cancellationToken);

            var taskStatusChangedEvent = new TaskStatusChangedEvent(
                Guid.NewGuid(),
                DateTimeOffset.UtcNow,
                taskResult.Entity.Id,
                linkagesSprintInfo!.Id, 
                linkagesSprintInfo.UserId, 
                TasksStatus.Created.Value.ToString());

            await publishEndpoint.Publish(taskStatusChangedEvent, cancellationToken);

            return ExecutionResult.Success(taskResult.Entity.Id);
        }
    }
}

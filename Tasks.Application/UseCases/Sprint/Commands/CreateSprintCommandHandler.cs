using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Common.Contracts.Events;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.DataAccess.Postgres;
using Tasks.Domain.Entities;
using Tasks.Domain.Specifications;
using ExecutionResult = TaskManagerSystem.Common.Implementation.ExecutionResult;

namespace Tasks.Application.UseCases.Sprint.Commands
{
    public class CreateSprintCommandHandler(TaskDbContext dbContext, IPublishEndpoint publishEndpoint) : IRequestHandler<CreateSprintCommand, IExecutionResult<long>>
    {
        public async Task<IExecutionResult<long>> Handle(CreateSprintCommand request, CancellationToken cancellationToken)
        {

            var fieldActivities = await dbContext.FieldActivities
                                                 .Where(FieldActivitySpecification.ByIds(request.Dto.FieldActivityIds))
                                                 .ToListAsync(cancellationToken);

            var sprintResult = SprintEntity.Create(
                request.UserId,
                request.Dto.Name,
                request.Dto.Description,
                fieldActivities);

            if (sprintResult.IsFailure)
                return ExecutionResult.Failure<long>(sprintResult.Error);

            var startDate = DateTimeOffset.UtcNow.Date;

            var dayWeekCount = 7;

            for(int weekIndex = 0; weekIndex < request.Dto.WeekCount; weekIndex++)
            {
                var weekStart = startDate.AddDays(weekIndex * dayWeekCount);
                var weekEnd = weekStart.AddDays(dayWeekCount - 1);

                var weekResult = SprintWeekEntity.Create(
                    sprintResult.Value,
                    weekIndex + 1,
                    weekStart,
                    weekEnd);

                if(weekResult.IsFailure)
                    return ExecutionResult.Failure<long>(weekResult.Error);

                sprintResult.Value.AddWeek(weekResult.Value);
            }
            
            sprintResult.Value.SetStartDate(startDate);
            sprintResult.Value.SetEndDate(sprintResult.Value.SprintWeeks[sprintResult.Value.SprintWeeks.Count - 1].EndDate);

            var createdSprint = await dbContext.AddAsync(sprintResult.Value, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            await publishEndpoint.Publish(new CreatedNewSprint(createdSprint.Entity.Id, createdSprint.Entity.UserId, createdSprint.Entity.Name.Name), cancellationToken);

            return ExecutionResult.Success(createdSprint.Entity.Id);
        }
    }
}

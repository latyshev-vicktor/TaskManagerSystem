using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Common.Contracts.Events;
using TaskManagerSystem.Common.Interfaces;
using Tasks.Application.Services;
using Tasks.DataAccess.Postgres;
using Tasks.Domain.Entities;
using Tasks.Domain.Specifications;
using ExecutionResult = TaskManagerSystem.Common.Implementation.ExecutionResult;

namespace Tasks.Application.UseCases.Sprint.Commands
{
    public class CreateSprintCommandHandler(
        TaskDbContext dbContext,
        IOutboxMessageService outboxMessageService) : IRequestHandler<CreateSprintCommand, IExecutionResult<Guid>>
    {
        public async Task<IExecutionResult<Guid>> Handle(CreateSprintCommand request, CancellationToken cancellationToken)
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
                return ExecutionResult.Failure<Guid>(sprintResult.Error);

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
                    return ExecutionResult.Failure<Guid>(weekResult.Error);

                sprintResult.Value.AddWeek(weekResult.Value);
            }
            
            sprintResult.Value.SetStartDate(startDate);
            sprintResult.Value.SetEndDate(sprintResult.Value.SprintWeeks[sprintResult.Value.SprintWeeks.Count - 1].EndDate);

            await dbContext.AddAsync(sprintResult.Value, cancellationToken);
            var createdSprint = sprintResult.Value;

            await outboxMessageService.Add(new CreatedNewSprint(createdSprint.Id, createdSprint.UserId, createdSprint.Name.Name));
            await dbContext.SaveChangesAsync(cancellationToken);

            return ExecutionResult.Success(createdSprint.Id);
        }
    }
}

using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.DataAccess.Postgres;
using Tasks.Domain.Entities;
using Tasks.Domain.Specifications;

namespace Tasks.Application.UseCases.Sprint.Commands
{
    public class CreateSprintCommandHandler(TaskDbContext dbContext) : IRequestHandler<CreateSprintCommand, IExecutionResult<long>>
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

            var startDate = DateTimeOffset.Now.Date;

            var weeksResult = Enumerable.Range(0, request.Dto.WeekCount)
                .Select(index =>
                {
                    var weekStart = startDate.AddDays(index * 7);
                    return SprintWeekEntity.Create(
                        sprintResult.Value,
                        index + 1,
                        weekStart,
                        weekStart.AddDays(6));
                });

            var failedWeek = weeksResult.FirstOrDefault(r => r.IsFailure);
            if(failedWeek != null)
                return ExecutionResult.Failure<long>(failedWeek.Error);

            foreach(var week in weeksResult)
            {
                sprintResult.Value.AddWeek(week.Value);
            }
            
            var createdSprint = await dbContext.AddAsync(sprintResult.Value, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return ExecutionResult.Success(createdSprint.Entity.Id);
        }
    }
}

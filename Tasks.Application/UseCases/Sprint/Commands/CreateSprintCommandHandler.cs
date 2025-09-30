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

            foreach (var target in request.Dto.Targets)
            {
                var targetResult = TargetEntity.Create(
                    target.Name,
                    sprintResult.Value);

                if (targetResult.IsFailure)
                    return ExecutionResult.Failure<long>(targetResult.Error);

                sprintResult.Value.AddTarget(targetResult.Value);
            }

            var createdSprint = await dbContext.AddAsync(sprintResult.Value, cancellationToken);

            await dbContext.SaveChangesAsync(cancellationToken);

            return ExecutionResult.Success(createdSprint.Entity.Id);
        }
    }
}

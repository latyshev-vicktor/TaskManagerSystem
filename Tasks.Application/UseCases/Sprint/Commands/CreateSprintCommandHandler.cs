using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Common.Contracts.Events.Analytics.v1;
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
                fieldActivities,
                request.Dto.WeekCount);

            if (sprintResult.IsFailure)
                return ExecutionResult.Failure<Guid>(sprintResult.Error);

            await dbContext.AddAsync(sprintResult.Value, cancellationToken);
            var createdSprint = sprintResult.Value;

            await outboxMessageService.Add(new CreatedNewSprint(Guid.NewGuid(), DateTime.UtcNow, createdSprint.Id, createdSprint.UserId, createdSprint.Name.Name));

            await dbContext.SaveChangesAsync(cancellationToken);

            return ExecutionResult.Success(createdSprint.Id);
        }
    }
}

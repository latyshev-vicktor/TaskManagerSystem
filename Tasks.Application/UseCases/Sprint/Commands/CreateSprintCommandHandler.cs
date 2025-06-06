﻿using MediatR;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.DataAccess.Postgres;
using Tasks.Domain.Entities;

namespace Tasks.Application.UseCases.Sprint.Commands
{
    public class CreateSprintCommandHandler(TaskDbContext dbContext) : IRequestHandler<CreateSprintCommand, IExecutionResult<long>>
    {
        public async Task<IExecutionResult<long>> Handle(CreateSprintCommand request, CancellationToken cancellationToken)
        {
            var sprintResult = SprintEntity.Create(
                request.UserId,
                request.Dto.Name,
                request.Dto.Description,
                request.Dto.FieldActivityId,
                request.Dto.StartDate,
                request.Dto.EndDate);

            if (sprintResult.IsFailure)
                return ExecutionResult.Failure<long>(sprintResult.Error);

            var createdSprint = await dbContext.AddAsync(sprintResult.Value, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return ExecutionResult.Success(createdSprint.Entity.Id);
        }
    }
}

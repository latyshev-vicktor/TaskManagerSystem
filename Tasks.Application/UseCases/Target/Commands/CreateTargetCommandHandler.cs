﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.DataAccess.Postgres;
using Tasks.Domain.Entities;
using Tasks.Domain.Specifications;

namespace Tasks.Application.UseCases.Target.Commands
{
    public class CreateTargetCommandHandler(TaskDbContext dbContext) : IRequestHandler<CreateTargetCommand, IExecutionResult<long>>
    {
        public async Task<IExecutionResult<long>> Handle(CreateTargetCommand request, CancellationToken cancellationToken)
        {
            var sprint = await dbContext.Sprints
                                                .AsNoTracking()
                                                .FirstOrDefaultAsync(SprintSpecification.ById(request.Dto.SprintId), cancellationToken);

            var newTarget = TargetEntity.Create(request.Dto.Name, sprint!);

            var savedTarget = await dbContext.AddAsync(newTarget.Value, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return ExecutionResult.Success(savedTarget.Entity.Id);
        }
    }
}

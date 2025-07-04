﻿using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Common.Extensions;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.DataAccess.Postgres;
using Tasks.Domain.Errors;
using Tasks.Domain.Specifications;
using Tasks.Domain.ValueObjects;

namespace Tasks.Application.UseCases.Target.Commands
{
    public class CreateTargetValidator : RequestValidator<CreateTargetCommand>
    {
        private readonly TaskDbContext _dbContext; 

        public CreateTargetValidator(TaskDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(x => x.Dto.Name).MustBeValueObject(TargetName.Create);
            RuleFor(x => x.Dto.SprintId).NotNull().NotEqual(default(long)).WithMessage(TargetError.SprintNotBeNull().Message);
        }

        public override async Task<IExecutionResult> RequestValidateAsync(CreateTargetCommand request, CancellationToken cancellationToken)
        {
            var existSprint = await _dbContext.Sprints
                                              .AsNoTracking()
                                              .AnyAsync(SprintSpecification.ById(request.Dto.SprintId));

            if (existSprint == false)
                return ExecutionResult.Failure(SprintError.SprintNotFoundById());

            return ExecutionResult.Success();
        }
    }
}

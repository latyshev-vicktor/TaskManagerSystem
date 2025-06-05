using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Common.Errors;
using TaskManagerSystem.Common.Extensions;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.DataAccess.Postgres;
using Tasks.Domain.Errors;
using Tasks.Domain.Specifications;
using Tasks.Domain.ValueObjects;

namespace Tasks.Application.UseCases.Target.Commands
{
    public class UpdateTargetCommandValidator : RequestValidator<UpdateTargetCommand>
    {
        private readonly TaskDbContext _dbContext;

        public UpdateTargetCommandValidator(TaskDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(x => x.Dto.Name).MustBeValueObject(TargetName.Create);
            RuleFor(x => x.Dto.SprintId).NotNull().NotEqual(default(long)).WithMessage(TargetError.SprintNotBeNull().Message);
        }

        public override async Task<IExecutionResult> RequestValidateAsync(UpdateTargetCommand request, CancellationToken cancellationToken)
        {
            var existTarget = await _dbContext.Targets
                                              .AsNoTracking()
                                              .AnyAsync(TargetSpecification.ById(request.Dto.Id), cancellationToken);

            if (existTarget == false)
                return ExecutionResult.Failure(BaseEntityError.EntityNotFound("цель"));

            return ExecutionResult.Success();
        }
    }
}

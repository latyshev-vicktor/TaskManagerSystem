using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Common.Errors;
using TaskManagerSystem.Common.Extensions;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.DataAccess.Postgres;
using Tasks.Domain.Errors;
using Tasks.Domain.ValueObjects;

namespace Tasks.Application.UseCases.Sprint.Commands
{
    public class UpdateSprintCommandValidator : RequestValidator<UpdateSprintCommand>
    {
        private readonly TaskDbContext _dbContext;
        public UpdateSprintCommandValidator(TaskDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(x => x.Dto.Name).MustBeValueObject(SprintName.Create);
            RuleFor(x => x.Dto.Description).MustBeValueObject(SprintDescription.Create);
            RuleFor(x => x.Dto.FieldActivityId).NotNull().NotEqual(0).WithMessage(SprintError.NotFoundFieldActivity().Message);
        }

        public async override Task<IExecutionResult> RequestValidateAsync(UpdateSprintCommand request, CancellationToken cancellationToken)
        {
            if (await _dbContext.Sprints.AnyAsync(x => x.Id == request.Dto.Id, cancellationToken) == false)
                return ExecutionResult.Failure(BaseEntityError.EntityNotFound("спринт"));

            return ExecutionResult.Success();
        }
    }
}

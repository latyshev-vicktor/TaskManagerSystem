using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Common.Errors;
using TaskManagerSystem.Common.Extensions;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.DataAccess.Postgres;
using Tasks.Domain.ValueObjects;

namespace Tasks.Application.UseCases.Sprint.Commands
{
    public class UpdateSprintCommandValidator : RequestValidator<UpdateSprintCommand>
    {
        private readonly TaskDbContext _dbContext;
        public UpdateSprintCommandValidator(TaskDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(x => x.Name).MustBeValueObject(SprintName.Create);
            RuleFor(x => x.Description).MustBeValueObject(SprintDescription.Create);
        }

        public async override Task<IExecutionResult> RequestValidateAsync(UpdateSprintCommand request, CancellationToken cancellationToken)
        {
            if (await _dbContext.Sprints.AnyAsync(x => x.Id == request.SprintId, cancellationToken) == false)
                return ExecutionResult.Failure(BaseEntityError.EntityNotFound("спринт"));

            return ExecutionResult.Success();
        }
    }
}

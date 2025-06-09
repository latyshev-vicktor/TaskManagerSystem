using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Common.Errors;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.DataAccess.Postgres;
using Tasks.DataAccess.Postgres.Configurations;
using Tasks.Domain.Errors;
using Tasks.Domain.Specifications;

namespace Tasks.Application.UseCases.FIeldActivity.Commands
{
    public class DeleteFieldActivityValidator : RequestValidator<DeleteFieldActivityCommand>
    {
        private readonly TaskDbContext _dbContext;
        public DeleteFieldActivityValidator(TaskDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(x => x.Id).NotEqual(0).WithMessage("Идентификатор не найден");
        }

        public override async Task<IExecutionResult> RequestValidateAsync(DeleteFieldActivityCommand request, CancellationToken cancellationToken)
        {
            var fieldActivity = await _dbContext.FieldActivities.FirstOrDefaultAsync(FieldActivitySpecification.ById(request.Id), cancellationToken);
            if (fieldActivity == null)
                return ExecutionResult.Failure(BaseEntityError.EntityNotFound("сфера деятельности"));

            if (fieldActivity.UserId != request.UserId)
                return ExecutionResult.Failure(FieldActivityError.NotBelongForCurrentUser());

            var existSprintByActivity = await _dbContext.Sprints.AnyAsync(SprintSpecification.ByFieldActivities([fieldActivity.Id]), cancellationToken);

            if (existSprintByActivity == true)
                return ExecutionResult.Failure(FieldActivityError.ExistSprintForDeleteCurrentActivity());

            return ExecutionResult.Success();
        }
    }
}

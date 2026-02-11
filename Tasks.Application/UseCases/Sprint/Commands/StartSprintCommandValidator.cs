using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Common.Errors;
using TaskManagerSystem.Common.Extensions;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.DataAccess.Postgres;
using Tasks.Domain.Errors;
using Tasks.Domain.Specifications;

namespace Tasks.Application.UseCases.Sprint.Commands
{
    public class StartSprintCommandValidator : RequestValidator<StartSprintCommand>
    {
        private readonly TaskDbContext _dbContext;
        public StartSprintCommandValidator(TaskDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(x => x.SprintId).NotNull().NotEmpty().CustomErrorMessage(SprintError.SprintNotFoundById());
            RuleFor(x => x.UserId).NotNull().NotEmpty().CustomErrorMessage(SprintError.UserNotEmpty());
        }

        public override async Task<IExecutionResult> RequestValidateAsync(StartSprintCommand request, CancellationToken cancellationToken)
        {
            var sprint = await _dbContext.Sprints
                    .Include(x => x.Targets)
                        .ThenInclude(x => x.Tasks)
                    .FirstOrDefaultAsync(SprintSpecification.ById(request.SprintId), cancellationToken);

            if(sprint == null)
                return ExecutionResult.Failure(BaseEntityError.EntityNotFound("спринт"));

            if(sprint.UserId != request.UserId)
                return ExecutionResult.Failure(SprintError.SprintDoesNotBelongForCurrentUser());

            if(sprint.UserId != request.UserId)
                return ExecutionResult.Failure(SprintError.SprintDoesNotBelongForCurrentUser());

            if (!sprint.Targets.Any())
                return ExecutionResult.Failure(SprintError.TargetsEmpty());

            var existTasks = sprint.Targets.SelectMany(x => x.Tasks).Any();

            if (!existTasks)
                return ExecutionResult.Failure(SprintError.TasksEmpty());

            return ExecutionResult.Success();
        }
    }
}

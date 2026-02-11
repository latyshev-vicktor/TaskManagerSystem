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

namespace Tasks.Application.UseCases.Task.Commands
{
    public class UpdateTaskCommandValidator : RequestValidator<UpdateTaskCommand>
    {
        private readonly TaskDbContext _dbContext;

        public UpdateTaskCommandValidator(TaskDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(x => x.Dto.Name).MustBeValueObject(TaskName.Create);
            RuleFor(x => x.Dto.Description).MustBeValueObject(TaskDescription.Create);
            RuleFor(x => x.Dto.TargetId).NotNull().NotEmpty().CustomErrorMessage(TaskError.TargetIdNotFound());
        }

        public async override Task<IExecutionResult> RequestValidateAsync(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var existTask = await _dbContext.Tasks
                .AsNoTracking()
                .AnyAsync(TaskSpecification.ById(request.Dto.Id), cancellationToken);

            if (!existTask)
                return ExecutionResult.Failure(BaseEntityError.EntityNotFound("задача"));

            return ExecutionResult.Success();
        }
    }
}

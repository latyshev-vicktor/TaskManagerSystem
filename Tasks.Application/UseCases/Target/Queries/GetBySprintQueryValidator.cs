using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.DataAccess.Postgres;
using Tasks.Domain.Errors;
using Tasks.Domain.Specifications;

namespace Tasks.Application.UseCases.Target.Queries
{
    public class GetBySprintQueryValidator : RequestValidator<GetBySprintQuery>
    {
        private readonly TaskDbContext _dbContext;

        public GetBySprintQueryValidator(TaskDbContext dbContext)
        {
            _dbContext = dbContext;

            RuleFor(x => x.SprintId).NotNull().NotEqual(default(long)).WithMessage(TargetError.SprintNotBeNull().Message);
        }

        public override async Task<IExecutionResult> RequestValidateAsync(GetBySprintQuery request, CancellationToken cancellationToken)
        {
            var existSprint = await _dbContext.Sprints
                                              .AsNoTracking()
                                              .AnyAsync(SprintSpecification.ById(request.SprintId), cancellationToken);

            if (existSprint == false)
                return ExecutionResult.Failure(TargetError.SprintNotBeNull());

            return ExecutionResult.Success();
        }
    }
}

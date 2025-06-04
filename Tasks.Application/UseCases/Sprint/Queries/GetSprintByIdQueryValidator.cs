using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.DataAccess.Postgres;
using Tasks.Domain.Errors;
using Tasks.Domain.Specifications;

namespace Tasks.Application.UseCases.Sprint.Queries
{
    public class GetSprintByIdQueryValidator : RequestValidator<GetSprintByIdQuery>
    {
        private readonly TaskDbContext _dbContext;

        public GetSprintByIdQueryValidator(TaskDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<IExecutionResult> RequestValidateAsync(GetSprintByIdQuery request, CancellationToken cancellationToken)
        {
            var existSprint = await _dbContext.Sprints
                                              .AsNoTracking()
                                              .AnyAsync(SprintSpecification.ById(request.Id), cancellationToken);

            if (existSprint == false)
                return ExecutionResult.Failure(SprintError.SprintNotFoundById());

            return ExecutionResult.Success();
        }
    }
}

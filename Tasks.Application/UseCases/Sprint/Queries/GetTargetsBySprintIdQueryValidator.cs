using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Common.Extensions;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.Application.Dto;
using Tasks.DataAccess.Postgres;
using Tasks.Domain.Errors;
using Tasks.Domain.Specifications;

namespace Tasks.Application.UseCases.Sprint.Queries
{
    public class GetTargetsBySprintIdQueryValidator : RequestValidator<GetTargetsBySprintIdQuery>
    {
        private readonly TaskDbContext _taskDbContext;
        public GetTargetsBySprintIdQueryValidator(TaskDbContext taskDbContext)
        {
            _taskDbContext = taskDbContext;

            RuleFor(x => x.SprintId).NotNull().NotEmpty().CustomErrorMessage(TargetError.SprintNotBeNull());
        }

        public async override Task<IExecutionResult> RequestValidateAsync(GetTargetsBySprintIdQuery request, CancellationToken cancellationToken)
        {
            var existSprint = await _taskDbContext.Sprints
                .AsNoTracking()
                .AnyAsync(SprintSpecification.ById(request.SprintId), cancellationToken);

            if (!existSprint)
            {
                return ExecutionResult.Failure<IReadOnlyList<TargetDto>>(SprintError.SprintNotFoundById());
            }

            return ExecutionResult.Success();
        }
    }
}

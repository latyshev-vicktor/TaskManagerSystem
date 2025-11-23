using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Common.Extensions;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.Application.Dto;
using Tasks.DataAccess.Postgres;
using Tasks.Domain.Errors;
using Tasks.Domain.Specifications;

namespace Tasks.Application.UseCases.SprintWeek.Queries
{
    public class GetWeeksBySprintIdQueryValidator : RequestValidator<GetWeeksBySprintIdQuery>
    {
        private readonly TaskDbContext _taskDbContext;

        public GetWeeksBySprintIdQueryValidator(TaskDbContext taskDbContext)
        {
            _taskDbContext = taskDbContext;

            RuleFor(x => x.SprintId).NotEqual(0).CustomErrorMessage(SprintError.SprintNotFoundById());
        }

        public async override Task<IExecutionResult> RequestValidateAsync(GetWeeksBySprintIdQuery request, CancellationToken cancellationToken)
        {
            var existSprint = await _taskDbContext.Sprints
                .AsNoTracking()
                .AnyAsync(SprintSpecification.ById(request.SprintId), cancellationToken);

            if (!existSprint)
                return ExecutionResult.Failure<List<SprintWeekDto>>(SprintError.SprintNotFoundById());

            return ExecutionResult.Success();
        }
    }
}

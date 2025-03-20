using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TaskManagerSystem.Common.Errors;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.DataAccess.Postgres;
using Tasks.Domain.Errors;
using Tasks.Domain.Specifications;
using Tasks.Domain.ValueObjects;

namespace Tasks.Application.UseCases.Sprint.Commands
{
    public class DeleteSprintValidator(TaskDbContext dbContext) : RequestValidator<DeleteSprintCommand>
    {
        public async override Task<IExecutionResult> RequestValidateAsync(DeleteSprintCommand request, CancellationToken cancellationToken)
        {
            var deletedSprint = await dbContext.Sprints.FirstOrDefaultAsync(SprintSpecification.ById(request.SprintId), cancellationToken);
            if (deletedSprint == null)
                return ExecutionResult.Failure(BaseEntityError.EntityNotFound("спринт"));

            if (deletedSprint.Status == SprintStatus.Completed)
                return ExecutionResult.Failure(SprintError.CompletedSprintNotBeDeleted());

            //if (long.TryParse(context.User?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value, out long userId))
            //{
            //    if (deletedSprint.UserId != userId)
            //        return ExecutionResult.Failure(SprintError.SprintDoesNotBelongForCurrentUser());
            //}
            //else
            //{
            //    return ExecutionResult.Failure(new Error(TaskManagerSystem.Common.Enums.ResultCode.BadRequest, "Не удалось получить id пользователя"));
            //}

            return ExecutionResult.Success();
        }
    }
}

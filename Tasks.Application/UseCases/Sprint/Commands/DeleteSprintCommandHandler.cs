using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.DataAccess.Postgres;
using Tasks.Domain.Specifications;

namespace Tasks.Application.UseCases.Sprint.Commands
{
    public class DeleteSprintCommandHandler(TaskDbContext dbContext) : IRequestHandler<DeleteSprintCommand, IExecutionResult>
    {
        public async Task<IExecutionResult> Handle(DeleteSprintCommand request, CancellationToken cancellationToken)
        {
            var deletedSprint = await dbContext.Sprints.FirstOrDefaultAsync(SprintSpecification.ById(request.SprintId), cancellationToken);
            deletedSprint!.Delete();

            return ExecutionResult.Success();
        }
    }
}

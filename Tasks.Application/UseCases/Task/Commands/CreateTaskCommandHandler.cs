using MediatR;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.DataAccess.Postgres;
using Tasks.Domain.Entities;

namespace Tasks.Application.UseCases.Task.Commands
{
    public class CreateTaskCommandHandler(TaskDbContext dbContext) : IRequestHandler<CreateTaskCommand, IExecutionResult<long>>
    {
        public async Task<IExecutionResult<long>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var newTask = TaskEntity.Create(request.CreateDto.Name, request.CreateDto.Description, request.CreateDto.TargetId!.Value);

            var taskResult = await dbContext.Tasks.AddAsync(newTask.Value, cancellationToken);
            await dbContext.SaveChangesAsync(cancellationToken);

            return ExecutionResult.Success(taskResult.Entity.Id);
        }
    }
}

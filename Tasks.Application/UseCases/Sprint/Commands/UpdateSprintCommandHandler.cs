using MediatR;
using Microsoft.EntityFrameworkCore;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.DataAccess.Postgres;
using Tasks.Domain.Entities;
using Tasks.Domain.Specifications;

namespace Tasks.Application.UseCases.Sprint.Commands
{
    public class UpdateSprintCommandHandler(TaskDbContext dbContext) : IRequestHandler<UpdateSprintCommand, IExecutionResult<long>>
    {
        public async Task<IExecutionResult<long>> Handle(UpdateSprintCommand request, CancellationToken cancellationToken)
        {
            var sprint = await dbContext.Sprints
                .Include(x => x.SprintFieldActivities)
                    .ThenInclude(x => x.FieldActivity)
                .AsSingleQuery()
                .FirstOrDefaultAsync(SprintSpecification.ById(request.Dto.Id), cancellationToken);

            sprint!.SetName(request.Dto.Name);
            sprint.SetDescription(request.Dto.Description);

            var existFieldActivities = sprint.SprintFieldActivities.Select(x => x.FieldActivity).ToList();
            var newFieldActivities = request.Dto.FieldActivities.Where(x => !existFieldActivities.Any(exist => exist!.Id == x.Id)).ToList();
            
            if (newFieldActivities.Count != 0)
            {
                var fieldActivitiesForCreate = await dbContext.FieldActivities
                    .Where(FieldActivitySpecification.ByIds(newFieldActivities.Select(x => x.Id).ToArray()))
                    .ToListAsync(cancellationToken);

                sprint.AddFieldActivities(fieldActivitiesForCreate);
            }

            var fieldActivityForDelete = existFieldActivities.Where(x => !request.Dto.FieldActivities.Any(f => f.Id == x!.Id)).ToList();
            if(fieldActivityForDelete.Count != 0)
            {
                sprint.RemoveFieldActivities(fieldActivityForDelete!);
            }
            
            await dbContext.SaveChangesAsync(cancellationToken);
            return ExecutionResult.Success(sprint.Id);
        }
    }
}

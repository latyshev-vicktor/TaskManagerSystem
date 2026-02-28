using AnalyticsService.DataAccess.Postgres;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AnalyticsService.Application.UseCases.Sprint.Commands
{
    public class UpdateSprintNameAnalyticsCommandHandler(AnalyticsDbContext dbContext) : IRequestHandler<UpdateSprintNameAnalyticsCommand>
    {
        public async Task Handle(UpdateSprintNameAnalyticsCommand request, CancellationToken cancellationToken)
        {
            var sprint = await dbContext
                .SprintAnalitycs
                .FirstOrDefaultAsync(x => x.SprintId == request.SprintId, cancellationToken);

            if(sprint == null)
            {
                throw new NullReferenceException("Не найден спринт по переданному Id");
            }

            sprint.UpdateName(request.NewName);
            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}

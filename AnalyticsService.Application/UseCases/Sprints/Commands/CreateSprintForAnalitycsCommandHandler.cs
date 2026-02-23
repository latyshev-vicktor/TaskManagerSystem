using AnalyticsService.DataAccess.Postgres;
using AnalyticsService.Domain.Entities.AnalitycsModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AnalyticsService.Application.UseCases.Sprints.Commands
{
    public class CreateSprintForAnalitycsCommandHandler(AnalyticsDbContext dbContext) : IRequestHandler<CreateSprintForAnalitycsCommand>
    {
        public async Task Handle(CreateSprintForAnalitycsCommand request, CancellationToken cancellationToken)
        {
            var existSprint = await dbContext.SprintAnalitycs
                .AsNoTracking()
                .AnyAsync(x => x.SprintId == request.SprintId, cancellationToken);

            if (existSprint)
            {
                return;
            }

            var newSprint = new SprintAnalyticsEntity(request.UserId, request.SprintId, request.Name);
            await dbContext.SprintAnalitycs.AddAsync(newSprint, cancellationToken);
        }
    }
}

using AnalyticsService.DataAccess.Postgres;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AnalyticsService.Application.UseCases.Tasks.Commands
{
    public class DeleteAnalyticsTaskCommandHandler(AnalyticsDbContext dbContext) : IRequestHandler<DeleteAnalyticsTaskCommand>
    {
        public async Task Handle(DeleteAnalyticsTaskCommand request, CancellationToken cancellationToken)
        {
            var task = await dbContext
                .SprintTaskAnalytics
                .FirstOrDefaultAsync(x => x.TaskId == request.TaskId, cancellationToken) ?? throw new NullReferenceException("Не найдена задача по переданному Id");
            
            dbContext.SprintTaskAnalytics.Remove(task);
        }
    }
}

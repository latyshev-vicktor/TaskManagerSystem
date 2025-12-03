using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Tasks.Application.Dto;
using Tasks.Application.Hubs;
using Tasks.Application.Interfaces;
using Tasks.DataAccess.Postgres;
using Tasks.Domain.DomainEvents;
using Tasks.Domain.Specifications;

namespace Tasks.Application.DomainEventHandlers
{
    public class SprintStatusUpdatedEventHandler(
        TaskDbContext dbContext,
        IHubContext<SprintNotificationHub, ISprintNotificationHubClient> hubContext) : INotificationHandler<SprintStatusUpdatedEvent>
    {
        public async Task Handle(SprintStatusUpdatedEvent notification, CancellationToken cancellationToken)
        {
            var sprint = await dbContext.Sprints
                .Where(SprintSpecification.ById(notification.Id))
                .Select(x => new SprintDto
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    CreatedDate = x.CreatedDate,
                    Name = x.Name.Name,
                    Description = x.Description.Description,
                    SprintStatus = new SprintStatusDto
                    {
                        Name = x.Status.Value,
                        Description = x.Status.Description
                    },
                    StartDate = x.StartDate,
                    EndDate = x.EndDate
                }).FirstOrDefaultAsync(cancellationToken);

            await hubContext.Clients.User(sprint!.UserId.ToString()).SprintStatusUpdated(sprint);
        }
    }
}

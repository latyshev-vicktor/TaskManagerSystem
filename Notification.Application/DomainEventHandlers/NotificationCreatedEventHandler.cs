using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Notification.Application.Dto;
using Notification.Application.HubClients;
using Notification.Application.Hubs;
using Notification.DataAccess.Postgres;
using Notification.Domain.DomainEvents;
using Notification.Domain.Specifications;

namespace Notification.Application.DomainEventHandlers
{
    public class NotificationCreatedEventHandler(
        NotificationDbContext dbContext,
        IHubContext<NotificationHub, INotificationHubClient> hubContext) : INotificationHandler<NotificationCreatedEvent>
    {
        public async Task Handle(NotificationCreatedEvent notification, CancellationToken cancellationToken)
        {
            var lasNotification = await dbContext.Notifications
                .OrderByDescending(x => x.CreatedDate)
                .Where(NotificationSpecification.ByUserId(notification.UserId) & NotificationSpecification.IsNotReaded())
                .Select(x => new NotificationDto
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    CreatedDate = x.CreatedDate,
                    IsRead = x.IsRead,
                    Title = x.Title,
                    Message = x.Message,
                    ReadDate = x.ReadDate,
                })
                .FirstOrDefaultAsync(cancellationToken);

           if(lasNotification != null)
           {
                await hubContext.Clients.Group($"user:{lasNotification.UserId}").Receive(lasNotification);
           }
        }
    }
}

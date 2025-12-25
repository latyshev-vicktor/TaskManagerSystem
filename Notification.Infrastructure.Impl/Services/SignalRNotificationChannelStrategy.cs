using Microsoft.AspNetCore.SignalR;
using Notification.Application.Dto;
using Notification.Application.HubClients;
using Notification.Application.Hubs;
using Notification.Application.Services;
using Notification.Domain.Entities;
using Notification.Domain.Enums;

namespace Notification.Infrastructure.Impl.Services
{
    public class SignalRNotificationChannelStrategy(IHubContext<NotificationHub, INotificationHubClient> hub) : INotificationChannelStrategy
    {
        public NotificationChannel Channel => NotificationChannel.SignalR;

        public async Task SendAsync(NotificationEntity notification, UserNotificationProfileEntity profile)
        {
            if (!profile.IsChannelEnabled(Channel))
                return;

            var notificationSenderDto = new NotificationDto
            {
                Title = notification.Title,
                Message = notification.Message,
                UserId = notification.UserId,
            };

            await hub.Clients.Group($"userId:{profile.UserId}").Receive(notificationSenderDto);
        }
    }
}

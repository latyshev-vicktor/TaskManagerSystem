using Notification.Application.Dto;

namespace Notification.Application.HubClients
{
    public interface INotificationHubClient
    {
        Task Receive(NotificationDto notification);
    }
}

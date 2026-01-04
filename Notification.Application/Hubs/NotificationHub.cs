using Microsoft.AspNetCore.SignalR;
using Notification.Application.HubClients;
using System.Security.Claims;

namespace Notification.Application.Hubs
{
    public class NotificationHub : Hub<INotificationHubClient>
    {
        public async override Task OnConnectedAsync()
        {
            var userId = Context.User?.FindFirst(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            if(!string.IsNullOrWhiteSpace(userId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, $"user:{userId}");
            }
            await base.OnConnectedAsync();
        }
    }
}

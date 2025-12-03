using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Tasks.Application.Interfaces;

namespace Tasks.Application.Hubs
{
    [Authorize]
    public sealed class SprintNotificationHub : Hub<ISprintNotificationHubClient>
    {
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Tasks.Application.Hubs.Interfaces;
using Tasks.Domain.ValueObjects;

namespace Tasks.Application.Hubs
{
    public class SprintHub : Hub<ISprintHubClient>
    {
    }
}

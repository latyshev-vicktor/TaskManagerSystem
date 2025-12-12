using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Tasks.Api.HubsProviders
{
    public class UserProvider : IUserIdProvider
    {
        public string? GetUserId(HubConnectionContext connection)
        {
            return connection.User?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        }
    }
}

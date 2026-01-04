using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notification.Application.UseCases.UserNotificationProfile.Commands;
using Notification.Application.UseCases.UserNotificationProfile.Dto;
using Notification.Application.UseCases.UserNotificationProfile.Queries;
using System.Security.Principal;
using TaskManagerSystem.Common.Extensions;

namespace Notification.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserNotificationProfileController(IPrincipal principal, IMediator mediator) : ControllerBase
    {
        [HttpGet("user-notification-settings")]
        public async Task<IActionResult> GetUserNotificationSettings()
        {
            var userId = principal.GetUserId();
            var query = new GetUserNotificationSettingsQuery(userId);
            var result = await mediator.Send(query);

            return Ok(result.Value);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserNotificationProfile([FromBody]UpdatedUserNotificationProfileDto request)
        {
            var command = new UpdatedUserNotificationProfileCommand(request);
            var result = await mediator.Send(command, new CancellationTokenSource().Token);

            return NoContent();
        }
    }
}

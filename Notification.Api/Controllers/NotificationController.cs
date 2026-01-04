using MediatR;
using Microsoft.AspNetCore.Mvc;
using Notification.Application.UseCases.Notification.Commands;
using Notification.Application.UseCases.Notification.Queries;
using System.Security.Principal;
using TaskManagerSystem.Common.Extensions;

namespace Notification.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController(IMediator mediator, IPrincipal principal) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult> GetAllByUserId()
        {
            var userId = principal.GetUserId();
            var query = new GetAllNotificationByUserIdQuery(userId);
            var result = await mediator.Send(query);

            return Ok(result.Value);
        }

        [HttpGet("unread-count")]
        public async Task<ActionResult> GetUnreadCount()
        {
            var userId = principal.GetUserId();
            var query = new GetUnreadCountQuery(userId);
            var result = await mediator.Send(query);

            return Ok(result.Value);
        }

        [HttpPatch("{id:Guid}/read")]
        public async Task<ActionResult> MarkAsRead(Guid id)
        {
            var command = new MarkAsReadCommand(id);
            var result = await mediator.Send(command);

            return result.Match(() => NoContent(), error => BadRequest(result.Error));
        }

        [HttpPatch("read-all")]
        public async Task<ActionResult> MarkAllAsRead()
        {
            var userId = principal.GetUserId();
            var command = new MarkAllAsReadCommand(userId);
            var result = await mediator.Send(command);

            return NoContent();
        }
    }
}

using AuthenticationService.Domain.ValueObjects.User;
using FluentEmail.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

namespace AuthenticationService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class TestController(IConfiguration configuration) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get(string subject, string body, string toEmail)
        {
            return Ok();
        }
    }
}

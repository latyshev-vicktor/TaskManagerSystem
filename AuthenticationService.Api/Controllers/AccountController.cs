using AuthenticationService.Api.Contracts;
using AuthenticationService.Application.UseCases.User.Commands;
using AuthenticationService.Application.UseCases.User.Dto;
using AuthenticationService.Application.UseCases.User.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Security.Principal;
using TaskManagerSystem.Common.Enums;
using TaskManagerSystem.Common.Extensions;
using TaskManagerSystem.Common.Options;

namespace AuthenticationService.Api.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AccountController(IMediator mediator, IOptions<JwtSettings> options, IPrincipal principal) : ControllerBase
    {
        private readonly JwtSettings _jwtSettings = options.Value;

        [HttpPost("registration")]
        public async Task<IActionResult> Registration([FromBody]CreateUserDto dto)
        {
            var command = new CreateUserRequest(dto);
            var result = await mediator.Send(command);

            return result.Match(() => NoContent(), error => BadRequest(result.Error.Message));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginFormRequest request)
        {
            var query = new LoginQuery(request.Email, request.Password);
            var response = await mediator.Send(query);

            SetResponseCookies(response.Value.RefreshToken);

            return Ok(new { response.Value.AccessToken});
        }

        [HttpGet("refresh-token")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refresh_token"];
            if (string.IsNullOrWhiteSpace(refreshToken))
            {
                return Unauthorized();
            }

            var query = new RefreshTokenQuery(refreshToken);
            var result = await mediator.Send(query);

            if (result.IsFailure && result.Error.Result == ResultCode.UnAuthorize)
            {
                return Unauthorized();
            }

            SetResponseCookies(result.Value.RefreshToken);

            return Ok(new { accessToken = result.Value.AccessToken });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var userIdClaims = User?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if(!Guid.TryParse(userIdClaims, out var userId))
            {
                return Unauthorized();
            }

            var query = new LogoutQuery(userId);
            var result = await mediator.Send(query);

            if (result.IsFailure)
                return BadRequest(result.Error.Message);

            Response.Cookies.Delete("refresh_token");

            return Ok();
        }

        private void SetResponseCookies(string refreshToken)
        {
            var cookieOption = new CookieOptions()
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpiredMinute),
                Secure = true
            };

            Response.Cookies.Append("refresh_token", refreshToken, cookieOption);
        }
    }
}

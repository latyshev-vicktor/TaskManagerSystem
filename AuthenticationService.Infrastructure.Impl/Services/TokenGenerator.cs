using AuthenticationService.Application.Services;
using AuthenticationService.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskManagerSystem.Common.Options;

namespace AuthenticationService.Infrastructure.Impl.Services
{
    public class TokenGenerator(IOptions<JwtSettings> jwtOptions) : ITokenGenerator
    {
        private readonly JwtSettings _jwtSettings = jwtOptions.Value;

        public string GenerateToken(UserEntity user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Email, user.Email.Value!),
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new("userName", user.UserName.Value),
                new("firstName", user.FullName.FirstName),
                new("lasName", user.FullName.LastName)
            };

            foreach (var role in user.Roles)
                claims.Add(new("Roles", role.Name.Name));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.AccessTokenExpiredMinute),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

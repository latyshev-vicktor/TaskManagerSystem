using AuthenticationService.Application.Services;
using AuthenticationService.Domain.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TaskManagerSystem.Common.Extensions;
using TaskManagerSystem.Common.Options;

namespace AuthenticationService.Infrastructure.Impl.Services
{
    public class TokenService(IDistributedCache cache, IOptions<JwtSettings> jwtSettings) : ITokenService
    {
        private readonly JwtSettings _jwtSettings = jwtSettings.Value;
        private const string ACCESS_KEY = "access";
        private const string REFRESH_KEY = "refresh";
        private const string USER_ACCESS_KEY = "user:access";
        private const string USER_REFRESH_KEY = "user:refresh";

        public async Task<(string AccessToken, string RefreshToken)> GenerateTokenAsync(UserEntity user)
        {
            await RevokeAllTokensForUser(user.Id);

            var jti = Guid.NewGuid().ToString();

            var accessToken = GenerateAccessToken(user, jti);
            var refreshToken = GenerateRefreshToken();

            var accessKey = $"{ACCESS_KEY}:{jti}";
            var refreshKey = $"{REFRESH_KEY}:{refreshToken}";
            var userAccessKey = $"{USER_ACCESS_KEY}:{user.Id}";
            var userRefreshKey = $"{USER_REFRESH_KEY}:{user.Id}";

            await Task.WhenAll(
                cache.SetAsync(accessKey, user.Id.ToString(), TimeSpan.FromMinutes(_jwtSettings.AccessTokenExpiredMinute)),
                cache.SetAsync(refreshKey, user.Id.ToString(), TimeSpan.FromMinutes(_jwtSettings.RefreshTokenExpiredMinute)),
                cache.SetAsync(userAccessKey, jti, TimeSpan.FromMinutes(_jwtSettings.AccessTokenExpiredMinute)),
                cache.SetAsync(userRefreshKey, refreshToken, TimeSpan.FromMinutes(_jwtSettings.RefreshTokenExpiredMinute))
                );

            return (accessToken, refreshToken);
        }

        public async Task<bool> ValidateRefreshTokenAsync(string refreshToken)
        {
            var refreshKey = $"{REFRESH_KEY}:{refreshToken}";
            var userId = await cache.GetStringAsync(refreshKey);
            return !string.IsNullOrEmpty(userId);
        }

        public async Task RevokeAllTokensForUser(Guid userId)
        {
            var userAccessKey = $"{USER_ACCESS_KEY}:{userId}";
            var userRefreshKey = $"{USER_REFRESH_KEY}:{userId}";

            var accessToken = await cache.GetAsync<string>(userAccessKey);
            var refreshToken = await cache.GetAsync<string>(userRefreshKey);

            if (!string.IsNullOrWhiteSpace(accessToken))
            {
                await cache.RemoveAsync($"{ACCESS_KEY}:{accessToken}");
            }

            if (!string.IsNullOrWhiteSpace(refreshToken))
            {
                await cache.RemoveAsync($"{REFRESH_KEY}:{refreshToken}");
            }

            await cache.RemoveAsync(userAccessKey);
            await cache.RemoveAsync(userRefreshKey);
        }

        private string GenerateAccessToken(UserEntity user, string jti)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Email, user.Email.Value!),
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new("userName", user.UserName.Value),
                new("firstName", user.FullName.FirstName),
                new("lasName", user.FullName.LastName),
                new(JwtRegisteredClaimNames.Jti, jti)
            };

            foreach (var role in user.Roles)
                claims.Add(new("Roles", role.Name.Name));

            foreach (var audience in _jwtSettings.Audiences)
                claims.Add(new(JwtRegisteredClaimNames.Aud, audience));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expires = DateTime.UtcNow.AddHours(_jwtSettings.AccessTokenExpiredMinute);

            var tokenSecutity = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
                //audience: _jwtSettings.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: creds);


            return new JwtSecurityTokenHandler().WriteToken(tokenSecutity);
        }

        public async Task<Guid?> GetUserIdByRefreshToken(string refreshToken)
        {
            var refreshKey = $"{REFRESH_KEY}:{refreshToken}";
            var userId = await cache.GetAsync<Guid?>(refreshKey);
            return userId;
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var randomGenerator = RandomNumberGenerator.Create();
            randomGenerator.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}

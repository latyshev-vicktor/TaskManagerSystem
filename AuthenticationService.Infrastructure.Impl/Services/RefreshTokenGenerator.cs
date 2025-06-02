using AuthenticationService.Application.Services;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using TaskManagerSystem.Common.Extensions;
using TaskManagerSystem.Common.Options;

namespace AuthenticationService.Infrastructure.Impl.Services
{
    public class RefreshTokenGenerator(IDistributedCache cache, IOptions<JwtSettings> jwtSettingsOptions) : IRefreshTokenGenerator
    {
        private readonly JwtSettings _jwtSettings = jwtSettingsOptions.Value;

        private const string TOKEN_PREFIX = "refresh:token";
        private const string USER_PREFIX = "refresh:user";

        public async Task<string> GenerateAsync(long userId)
        {
            var secureToken = GenerateSecureToken();

            var userKey = $"{USER_PREFIX}:{userId}";
            var tokenKey = $"{TOKEN_PREFIX}:{secureToken}";

            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_jwtSettings.RefreshTokenExpiredMinute)
            };

            var oldToken = await cache.GetAsync<string>(userKey);
            if (!string.IsNullOrWhiteSpace(oldToken))
            {
                var oldTokenKey = $"{TOKEN_PREFIX}:{oldToken}";
                await cache.RemoveAsync(userKey);
                await cache.RemoveAsync(oldTokenKey);
            }

            await cache.SetAsync(userKey, secureToken, TimeSpan.FromMinutes(_jwtSettings.RefreshTokenExpiredMinute));
            await cache.SetAsync(tokenKey, secureToken, TimeSpan.FromMinutes(_jwtSettings.AccessTokenExpiredMinute));

            return secureToken;
        }

        public async Task<string?> GetTokenByUserIdAsync(long userId)
        {
            var userKey = $"{USER_PREFIX}:{userId}";
            return await cache.GetAsync<string?>(userKey);
        }

        public async Task<long?> GetUserIdByTokenAsync(string refreshToken)
        {
            var tokenKey = $"{TOKEN_PREFIX}:{refreshToken}";
            return await cache.GetAsync<long?>(tokenKey);
        }

        public async Task RevokeAsync(long userId)
        {
            var userKey = $"{USER_PREFIX}:{userId}";
            var token = await cache.GetAsync<string>(userKey);
            if (!string.IsNullOrWhiteSpace(token))
            {
                await cache.RemoveAsync($"{TOKEN_PREFIX}:{token}");
            }

            await cache.RemoveAsync(userKey);
        }

        private string GenerateSecureToken()
        {
            var randomBytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }
    }
}

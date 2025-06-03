using AuthenticationService.Domain.Entities;

namespace AuthenticationService.Application.Services
{
    public interface ITokenService
    {
        Task<(string AccessToken, string RefreshToken)> GenerateTokenAsync(UserEntity user);
        Task<long?> GetUserIdByRefreshToken(string refreshToken);
        Task RevokeAccessTokenAsync(long userId);
        Task RevokeAllTokensForUser(long userId);
        Task RevokeRefreshToken(string refreshToken);
        Task<bool> ValidateRefreshTokenAsync(string refreshToken);
    }
}

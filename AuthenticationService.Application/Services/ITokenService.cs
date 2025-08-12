using AuthenticationService.Domain.Entities;

namespace AuthenticationService.Application.Services
{
    public interface ITokenService
    {
        Task<(string AccessToken, string RefreshToken)> GenerateTokenAsync(UserEntity user);
        Task<long?> GetUserIdByRefreshToken(string refreshToken);
        Task RevokeAllTokensForUser(long userId);
        Task<bool> ValidateRefreshTokenAsync(string refreshToken);
    }
}

using AuthenticationService.Domain.Entities;

namespace AuthenticationService.Application.Services
{
    public interface ITokenService
    {
        Task<(string AccessToken, string RefreshToken)> GenerateTokenAsync(UserEntity user);
        Task<Guid?> GetUserIdByRefreshToken(string refreshToken);
        Task RevokeAllTokensForUser(Guid userId);
        Task<bool> ValidateRefreshTokenAsync(string refreshToken);
    }
}

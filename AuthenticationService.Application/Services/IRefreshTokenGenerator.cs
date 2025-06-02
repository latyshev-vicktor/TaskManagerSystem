using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService.Application.Services
{
    public interface IRefreshTokenGenerator
    {
        Task<string> GenerateAsync(long userId);
        Task<long?> GetUserIdByTokenAsync(string refreshToken);
        Task<string?> GetTokenByUserIdAsync(long userId);
        Task RevokeAsync(long userId);
    }
}

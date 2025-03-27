using AuthenticationService.Domain.Entities;

namespace AuthenticationService.Application.Services
{
    public interface ITokenGenerator
    {
        string GenerateToken(UserEntity user);
    }
}

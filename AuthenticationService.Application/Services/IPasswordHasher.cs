namespace AuthenticationService.Application.Services
{
    public interface IPasswordHasher
    {
        string GenerateHash(string password);
        bool VerifyPassword(string password, string hashPassword);
    }
}

﻿using AuthenticationService.Application.Services;

namespace AuthenticationService.Infrastructure.Impl.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        public string GenerateHash(string password)
            => BCrypt.Net.BCrypt.HashPassword(password);

        public bool VerifyPassword(string password, string hashPassword)
            => BCrypt.Net.BCrypt.Verify(password, hashPassword);
    }
}

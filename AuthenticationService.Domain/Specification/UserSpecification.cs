using AuthenticationService.Domain.Entities;
using NSpecifications;

namespace AuthenticationService.Domain.Specification
{
    public static class UserSpecification
    {
        public static Spec<UserEntity> ByEmail(string email)
            => new(x => x.Email.Value == email);

        public static Spec<UserEntity> ByUserName(string userName)
            => new(x => x.UserName.Value == userName);

        public static Spec<UserEntity> ByPassword(string hashPassword)
            => new(x => x.PasswordHash == hashPassword);
    }
}

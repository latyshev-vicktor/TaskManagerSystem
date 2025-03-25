using AuthenticationService.Domain.SeedWork;

namespace AuthenticationService.Domain.Entities
{
    public class EmailVerificationTokenEntity : BaseEntity
    {
        public long UserId { get; private set; }
        public UserEntity? User { get; private set; }
        public DateTime CreateOnUtc { get; private set; }
        public DateTime ExpiredOnUtc { get; private set; }

        public EmailVerificationTokenEntity(long userId, DateTime createOnUtc, DateTime expiredOnUtc)
        {
            UserId = userId;
            CreateOnUtc = createOnUtc;
            ExpiredOnUtc = expiredOnUtc;
        }
    }
}

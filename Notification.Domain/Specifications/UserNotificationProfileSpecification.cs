using Notification.Domain.Entities;
using NSpecifications;

namespace Notification.Domain.Specifications
{
    public static class UserNotificationProfileSpecification
    {
        public static Spec<UserNotificationProfileEntity> ByUserId(long userId)
            => new(x => x.UserId == userId);
    }
}

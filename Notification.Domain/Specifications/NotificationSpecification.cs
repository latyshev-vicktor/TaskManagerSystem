using Notification.Domain.Entities;
using NSpecifications;

namespace Notification.Domain.Specifications
{
    public static class NotificationSpecification
    {
        public static Spec<NotificationEntity> ById(long id)
            => new(x => x.Id == id);

        public static Spec<NotificationEntity> IsReaded()
            => new(x => x.IsRead == true);

        public static Spec<NotificationEntity> IsNotReaded()
            => new(x => x.IsRead == false);

        public static Spec<NotificationEntity> ByUserId(long userId)
            => new(x => x.UserId == userId);
    }
}

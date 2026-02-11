using Notification.Domain.Enums;
using Notification.Domain.SeedWork;

namespace Notification.Domain.Entities
{
    public class UserNotificationProfileEntity(Guid userId, string email) : BaseEntity
    {
        public Guid UserId { get; private set; } = userId;
        public string Email { get; private set; } = email;
        public bool EnableEmail { get; private set; } = true;
        public bool EnableSignalR { get; private set; } = true;

        #region DDD-методы
        public bool IsChannelEnabled(NotificationChannel channel)
        {
            return channel switch
            {
                NotificationChannel.Email => EnableEmail,
                NotificationChannel.SignalR => EnableSignalR,
                _ => false
            };
        }

        public void SetEmail(string email)
        {
            if (Email == email)
                return;

            Email = email;
        }

        public void SetEnableEmail(bool enable)
        {
            if (EnableEmail == enable)
                return;

            EnableEmail = enable;
        }

        public void SetEnableSignalR(bool enable)
        {
            if (EnableSignalR == enable)
                return;

            EnableSignalR = enable;
        }
        #endregion
    }
}

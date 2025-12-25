using Notification.Domain.Enums;
using Notification.Domain.SeedWork;

namespace Notification.Domain.Entities
{
    public class UserNotificationProfileEntity : BaseEntity
    {
        public long UserId { get; private set; }
        public string Email { get; private set; }
        public bool EnableEmail { get; private set; }
        public bool EnableSignalR { get; private set; }

        public UserNotificationProfileEntity(long userId, string email)
        {
            UserId = userId;
            Email = email;
            EnableEmail = true;
            EnableSignalR = true;
        }

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

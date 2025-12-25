using Notification.Domain.Enums;

namespace Notification.Domain.ValueObjects
{
    public sealed class NotificationChannelValue(NotificationChannel channel)
    {
        public NotificationChannel Channel { get; private set; } = channel;
    }
}

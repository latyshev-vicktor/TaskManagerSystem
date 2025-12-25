using Notification.Application.Services;
using Notification.Domain.Entities;

namespace Notification.Infrastructure.Impl.Services
{
    public class NotificationSender(IEnumerable<INotificationChannelStrategy> strategies) : INotificationSender
    {
        public async Task Dispatch(NotificationEntity notification, UserNotificationProfileEntity userNotificationProfile)
        {
            var channels = notification.Channels
                .Select(x => x.Channel)
                .ToHashSet();

            foreach(var strategy in strategies)
            {
                if (!channels.Contains(strategy.Channel))
                    continue;

                await strategy.SendAsync(notification, userNotificationProfile);
            }
        }
    }
}

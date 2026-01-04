using Notification.Domain.Enums;
using Notification.Domain.Errors;
using Notification.Domain.SeedWork;
using Notification.Domain.ValueObjects;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;

namespace Notification.Domain.Entities
{
    public class NotificationEntity : BaseEntity
    {
        public string Title { get; private set; }
        public string Message { get; private set; }
        public bool IsRead { get; private set; }
        /// <summary>
        /// UserId из сервиса авторизации
        /// </summary>
        public long UserId { get; private set; }
        public DateTimeOffset? ReadDate { get; set; }
        public NotificationType Type { get; private set; }

        private List<NotificationChannelValue> _channels = [];
        public IReadOnlyList<NotificationChannelValue> Channels => _channels;

        #region Конструкторы
        protected NotificationEntity()
        {
            
        }

        private NotificationEntity(string title, string message, long userId, NotificationType type, NotificationChannel[] channels)
        {
            Title = title;
            Message = message;
            UserId = userId;
            IsRead = false;
            Type = type;

            _channels = [.. channels.Select(c => new NotificationChannelValue(c))];
        }
        #endregion

        #region DDD-методы
        public static IExecutionResult<NotificationEntity> Create(
            string title,
            string message,
            long userId,
            NotificationType type,
            NotificationChannel[] channels)
        {
            if(string.IsNullOrWhiteSpace(title))
            {
                return ExecutionResult.Failure<NotificationEntity>(NotificationError.TitleNotBeEmpty());
            }

            if (string.IsNullOrWhiteSpace(message))
            {
                return ExecutionResult.Failure<NotificationEntity>(NotificationError.MessageNotBeEmpty());
            }

            if (userId == 0)
            {
                return ExecutionResult.Failure<NotificationEntity>(NotificationError.UserIdNotBeEmpty());
            }

            return ExecutionResult.Success(new NotificationEntity(title, message, userId, type, channels));
        }

        public IExecutionResult MarkAsRead()
        {
            if(IsRead)
            {
                return ExecutionResult.Failure(NotificationError.NotifyAlreadReaded());
            }

            IsRead = true;
            ReadDate = DateTimeOffset.UtcNow;

            return ExecutionResult.Success();
        }

        #endregion
    }
}

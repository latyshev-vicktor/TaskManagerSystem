using Notification.Domain.DomainEvents;
using Notification.Domain.Enums;
using Notification.Domain.Errors;
using Notification.Domain.SeedWork;
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

        #region Конструкторы
        protected NotificationEntity()
        {
            
        }

        private NotificationEntity(string title, string message, long userId, NotificationType type)
        {
            Title = title;
            Message = message;
            UserId = userId;
            IsRead = false;
            Type = type;

            RiseDomainEvents(new NotificationCreatedEvent(UserId));
        }
        #endregion

        #region DDD-методы
        public static IExecutionResult<NotificationEntity> Create(
            string title,
            string message,
            long userId,
            NotificationType type)
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

            return ExecutionResult.Success(new NotificationEntity(title, message, userId, type));
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

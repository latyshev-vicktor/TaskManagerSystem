using TaskManagerSystem.Common.Enums;
using TaskManagerSystem.Common.Errors;

namespace Notification.Domain.Errors
{
    public static class UserNotificationProfileError
    {
        public static Error NotFoundByUserId()
            => new(ResultCode.BadRequest, "Не найдена настройка уведомлений для данного пользователя");

        public static Error NotBelongForCurrentUser()
            => new(ResultCode.BadRequest, "Данная настройка не пренадлежит текущему пользователю");
    }
}

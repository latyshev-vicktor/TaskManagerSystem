using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerSystem.Common.Enums;
using TaskManagerSystem.Common.Errors;

namespace Notification.Domain.Errors
{
    public static class NotificationError
    {
        public static Error TitleNotBeEmpty()
            => new(ResultCode.BadRequest, "Заголовок не может быть пустым");

        public static Error MessageNotBeEmpty()
           => new(ResultCode.BadRequest, "Текст уведомления не может быть пустым");

        public static Error NotifyAlreadReaded()
            => new(ResultCode.BadRequest, "Уведомление уже прочитано");

        public static Error NotifyNotBelongCurrentUser()
            => new(ResultCode.Forbidden, "Уведомление не принадлежит текущему пользователю");

        public static Error UserIdNotBeEmpty()
            => new(ResultCode.Forbidden, "Пользователь должен быть заполнен");
    }
}

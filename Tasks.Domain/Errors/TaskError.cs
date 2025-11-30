using TaskManagerSystem.Common.Enums;
using TaskManagerSystem.Common.Errors;

namespace Tasks.Domain.Errors
{
    public class TaskError
    {
        public static Error TargetIdNotFound()
            => new(ResultCode.BadRequest, "Не указана цель для задачи");

        public static Error WeekNotFound()
            => new(ResultCode.BadRequest, "Не указана неделя");

        public static Error TaskAlreadyCompleted()
            => new(ResultCode.BadRequest, "Задача уже завершена");
    }
}

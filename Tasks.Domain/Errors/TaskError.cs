using TaskManagerSystem.Common.Enums;
using TaskManagerSystem.Common.Errors;

namespace Tasks.Domain.Errors
{
    public class TaskError
    {
        public static Error TargetIdNotFound()
            => new(ResultCode.BadRequest, "Не указана цель для задачи");
    }
}

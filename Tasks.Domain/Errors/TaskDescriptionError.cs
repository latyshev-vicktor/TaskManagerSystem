using TaskManagerSystem.Common.Enums;
using TaskManagerSystem.Common.Errors;

namespace Tasks.Domain.Errors
{
    public class TaskDescriptionError
    {
        public static Error NotEmpty()
            => new(ResultCode.BadRequest, "Описание задачи не может быть пустым");
    }
}

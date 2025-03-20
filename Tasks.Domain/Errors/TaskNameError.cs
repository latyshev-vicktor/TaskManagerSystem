using TaskManagerSystem.Common.Enums;
using TaskManagerSystem.Common.Errors;

namespace Tasks.Domain.Errors
{
    public class TaskNameError
    {
        public static Error NotEmpty()
           => new(ResultCode.BadRequest, "Наименование задачи не может быть пустым");
    }
}

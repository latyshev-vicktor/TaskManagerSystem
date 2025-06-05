using TaskManagerSystem.Common.Enums;
using TaskManagerSystem.Common.Errors;

namespace Tasks.Domain.Errors
{
    public class TargetError
    {
        public static Error SprintNotBeNull()
            => new(ResultCode.BadRequest, "Спринт не может быть пустым");
    }
}

using TaskManagerSystem.Common.Enums;
using TaskManagerSystem.Common.Errors;

namespace Tasks.Domain.Errors
{
    public class TargetNameError
    {
        public static Error NotEmpty()
            => new(ResultCode.BadRequest, "Имя цели не может быть пустым");
    }
}

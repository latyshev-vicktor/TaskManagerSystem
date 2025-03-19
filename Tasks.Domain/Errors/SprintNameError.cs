using TaskManagerSystem.Common.Enums;
using TaskManagerSystem.Common.Errors;

namespace Tasks.Domain.Errors
{
    public class SprintNameError
    {
        public static Error NotEmpty()
            => new(ResultCode.BadRequest, "Наименование спринта не может быть пустым");
    }
}

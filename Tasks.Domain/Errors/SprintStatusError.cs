using TaskManagerSystem.Common.Enums;
using TaskManagerSystem.Common.Errors;

namespace Tasks.Domain.Errors
{
    public class SprintStatusError
    {
        public static Error NotEmpty()
            => new(ResultCode.BadRequest, "Статус не может быть пустым");

        public static Error NotCorrect()
            => new(ResultCode.BadRequest, "Передан некорреткный статус");
    }
}

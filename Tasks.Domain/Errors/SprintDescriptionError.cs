using TaskManagerSystem.Common.Enums;
using TaskManagerSystem.Common.Errors;

namespace Tasks.Domain.Errors
{
    public class SprintDescriptionError
    {
        public static Error NotEmpty()
            => new(ResultCode.BadRequest, "Описание спринта не может быть пустым");

        public static Error MaxLength()
            => new(ResultCode.BadRequest, "Превышена максимальная длина описания спринта");
    }
}

using TaskManagerSystem.Common.Enums;
using TaskManagerSystem.Common.Errors;

namespace Tasks.Domain.Errors
{
    public class SprintError
    {
        public static Error UserNotEmpty()
            => new(ResultCode.BadRequest, "Не найден пользователь для создания спринта");

        public static Error StartDateNotBeLessNow()
            => new(ResultCode.BadRequest, "Дата старта спринта не может быть меньше текущей даты");

        public static Error EndDateNotBeLessNow()
            => new(ResultCode.BadRequest, "Дата окончания спринта не может быть меньше текущей даты");
    }
}

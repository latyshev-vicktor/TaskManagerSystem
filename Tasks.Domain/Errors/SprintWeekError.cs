using TaskManagerSystem.Common.Enums;
using TaskManagerSystem.Common.Errors;

namespace Tasks.Domain.Errors
{
    public static class SprintWeekError
    {
        public static Error SprintIsNull()
            => new(ResultCode.BadRequest, "Спринт не может быть Null");

        public static Error WeekNumberNotBeZero()
            => new(ResultCode.BadRequest, "Номер недели не может быть меньше или равен нулю");
    }
}

using TaskManagerSystem.Common.Enums;
using TaskManagerSystem.Common.Errors;

namespace Tasks.Domain.Errors
{
    public static class FieldActivityError
    {
        public static Error DublicateNameForCurrentUser()
            => new(ResultCode.BadRequest, "Сфера деятельности с таким именем уже существует");

        public static Error NotBelongForCurrentUser()
            => new(ResultCode.BadRequest, "Сфера деятельности не принадлежит текущему пользователю");

        public static Error ExistSprintForDeleteCurrentActivity()
            => new(ResultCode.BadRequest, "Невозможно удалить сферу деятельности, так как существует связанный с ней спринт");
    }
}

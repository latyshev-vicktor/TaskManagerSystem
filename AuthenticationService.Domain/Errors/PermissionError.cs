using AuthenticationService.Domain.ValueObjects.Permission;
using TaskManagerSystem.Common.Enums;
using TaskManagerSystem.Common.Errors;

namespace AuthenticationService.Domain.Errors
{
    public class PermissionError
    {
        public static Error NameNotBeEmpty()
            => new Error(ResultCode.BadRequest, "Наименование права не может быть пустое");

        public static Error NameMaxLength()
            => new Error(ResultCode.BadRequest, "Максимальная длина наименование права" + " " + PermissionName.MAX_NAME_LENGTH);

        public static Error DescriptionEmpty()
            => new Error(ResultCode.BadRequest, "Описание права не может быть пустое");
    }
}

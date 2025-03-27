using AuthenticationService.Domain.ValueObjects.Role;
using TaskManagerSystem.Common.Enums;
using TaskManagerSystem.Common.Errors;

namespace AuthenticationService.Domain.Errors
{
    public class RoleError
    {
        public static Error NameNotBeEmpty()
            => new Error(ResultCode.BadRequest, "Наименование роли не может быть пустым");

        public static Error NameMaxLength()
            => new Error(ResultCode.BadRequest, "Максимальная длина наименование роли" + " " + RoleName.MAX_NAME_LENGTH);

        public static Error DescriptionEmpty()
            => new Error(ResultCode.BadRequest, "Описание роли не может быть пустым");
    }
}

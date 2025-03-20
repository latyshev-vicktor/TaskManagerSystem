using TaskManagerSystem.Common.Enums;

namespace TaskManagerSystem.Common.Errors
{
    public class BaseEntityError
    {
        public static Error EntityNotFound(string entityName)
            => new(ResultCode.NotFound, $"Сущность {entityName} не найдена");
    }
}

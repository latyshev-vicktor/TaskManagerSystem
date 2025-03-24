using AuthenticationService.Domain.SeedWork;
using AuthenticationService.Domain.ValueObjects.Permission;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;

namespace AuthenticationService.Domain.Entities
{
    public class PermissionEntity : BaseEntity
    {
        public PermissionName Name { get; private set; }
        public PermissionDescription Description { get; private set; }

        public List<RoleEntity> Roles { get; set; } = [];

        #region Конструкторы
        protected PermissionEntity()
        {

        }

        protected PermissionEntity(PermissionName name, PermissionDescription description)
        {
            Name = name;
            Description = description;
        }
        #endregion

        #region DDD-методы
        public static IExecutionResult Create(string name, string description)
        {
            var nameResult = PermissionName.Create(name);
            if (nameResult.IsFailure)
                return ExecutionResult.Failure(nameResult.Error);

            var descriptionResult = PermissionDescription.Create(description);
            if (descriptionResult.IsFailure)
                return ExecutionResult.Failure(descriptionResult.Error);

            return ExecutionResult.Success(new PermissionEntity(nameResult.Value, descriptionResult.Value));
        }

        public IExecutionResult SetName(string name)
        {
            var nameResult = PermissionName.Create(name);
            if (nameResult.IsFailure)
                return ExecutionResult.Failure(nameResult.Error);

            Name = nameResult.Value;

            return ExecutionResult.Success();
        }

        public IExecutionResult SetDescription(string description)
        {
            var descriptionResult = PermissionDescription.Create(description);
            if (descriptionResult.IsFailure)
                return ExecutionResult.Failure(descriptionResult.Error);

            Description = descriptionResult.Value;

            return ExecutionResult.Success();
        }
        #endregion
    }
}

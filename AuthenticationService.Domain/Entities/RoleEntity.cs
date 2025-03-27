using AuthenticationService.Domain.SeedWork;
using AuthenticationService.Domain.ValueObjects.Role;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;

namespace AuthenticationService.Domain.Entities
{
    public class RoleEntity : BaseEntity
    {
        public RoleName Name { get; private set; }
        public RoleDescription Description { get; private set; }
        public List<UserEntity> Users { get; set; } = [];
        public List<PermissionEntity> Permissions { get; set; } = [];
        public bool IsDefault { get; private set; }

        #region Конструкторы
        protected RoleEntity() { }

        protected RoleEntity(RoleName name, RoleDescription description, bool isDefault)
        {
            Name = name;
            Description = description;
            IsDefault = isDefault;
        }
        #endregion

        #region DDD-методы
        public IExecutionResult<RoleEntity> Create(string name, string description, bool isDefault = false)
        {
            var nameResult = RoleName.Create(name);
            if (nameResult.IsFailure)
                return ExecutionResult.Failure<RoleEntity>(nameResult.Error);

            var descriptionResult = RoleDescription.Create(description);
            if (descriptionResult.IsFailure)
                return ExecutionResult.Failure<RoleEntity>(descriptionResult.Error);

            return ExecutionResult.Success(new RoleEntity(nameResult.Value, descriptionResult.Value, isDefault));
        }

        public IExecutionResult SetName(string name)
        {
            var nameResult = RoleName.Create(name);
            if (nameResult.IsFailure)
                return ExecutionResult.Failure(nameResult.Error);

            Name = nameResult.Value;
            return ExecutionResult.Success();
        }

        public IExecutionResult SetDescription(string name)
        {
            var descriptionResult = RoleDescription.Create(name);
            if (descriptionResult.IsFailure)
                return ExecutionResult.Failure(descriptionResult.Error);

            Description = descriptionResult.Value;
            return ExecutionResult.Success();
        }
        #endregion
    }
}

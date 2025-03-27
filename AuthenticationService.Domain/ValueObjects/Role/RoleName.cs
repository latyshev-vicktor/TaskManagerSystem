using AuthenticationService.Domain.Errors;
using CSharpFunctionalExtensions;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;

namespace AuthenticationService.Domain.ValueObjects.Role
{
    public class RoleName : ValueObject
    {
        public const int MAX_NAME_LENGTH = 20;

        public string Name { get; }

        protected RoleName() { }

        protected RoleName(string name)
        {
            Name = name;
        }

        public static IExecutionResult<RoleName> Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return ExecutionResult.Failure<RoleName>(RoleError.NameNotBeEmpty());

            if (name.Length > MAX_NAME_LENGTH)
                return ExecutionResult.Failure<RoleName>(RoleError.NameMaxLength());

            return ExecutionResult.Success(new RoleName(name));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
        }
    }
}

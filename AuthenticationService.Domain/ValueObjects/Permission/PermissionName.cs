using AuthenticationService.Domain.Errors;
using CSharpFunctionalExtensions;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;

namespace AuthenticationService.Domain.ValueObjects.Permission
{
    public class PermissionName : ValueObject
    {
        public const int MAX_NAME_LENGTH = 20;

        public string Name { get; }
        protected PermissionName()
        {

        }

        protected PermissionName(string name)
        {
            Name = name;
        }

        public static IExecutionResult<PermissionName> Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return ExecutionResult.Failure<PermissionName>(PermissionError.NameNotBeEmpty());

            if (name.Length > MAX_NAME_LENGTH)
                return ExecutionResult.Failure<PermissionName>(PermissionError.NameMaxLength());

            return ExecutionResult.Success(new PermissionName(name));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
        }
    }
}

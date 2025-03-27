using AuthenticationService.Domain.Errors;
using CSharpFunctionalExtensions;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;

namespace AuthenticationService.Domain.ValueObjects.Permission
{
    public class PermissionDescription : ValueObject
    {
        public string Description { get; }
        protected PermissionDescription()
        {

        }

        protected PermissionDescription(string description)
        {
            Description = description;
        }

        public static IExecutionResult<PermissionDescription> Create(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                return ExecutionResult.Failure<PermissionDescription>(PermissionError.DescriptionEmpty());

            return ExecutionResult.Success(new PermissionDescription(description));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Description;
        }
    }
}

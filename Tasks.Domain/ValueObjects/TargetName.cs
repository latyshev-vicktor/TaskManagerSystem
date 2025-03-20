using CSharpFunctionalExtensions;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.Domain.Errors;

namespace Tasks.Domain.ValueObjects
{
    public class TargetName : ValueObject
    {
        public string Name { get; }
        protected TargetName() { }

        protected TargetName(string name)
        {
            Name = name;
        }

        public static IExecutionResult<TargetName> Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return ExecutionResult.Failure<TargetName>(TargetNameError.NotEmpty());

            return ExecutionResult.Success(new TargetName(name));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
        }
    }
}

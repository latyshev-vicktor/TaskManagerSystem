using CSharpFunctionalExtensions;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.Domain.Errors;

namespace Tasks.Domain.ValueObjects
{
    public class SprintDescription : ValueObject
    {
        public const int MAX_LENGTH_DESCRIPTION = 200;

        public string Description { get; }

        protected SprintDescription() { }

        protected SprintDescription(string description)
        {
            Description = description;
        }

        public static IExecutionResult<SprintDescription> Create(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                return ExecutionResult.Failure<SprintDescription>(SprintDescriptionError.NotEmpty());

            if (description.Length > MAX_LENGTH_DESCRIPTION)
                return ExecutionResult.Failure<SprintDescription>(SprintDescriptionError.MaxLength());

            return ExecutionResult.Success(new SprintDescription(description));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Description;
        }
    }
}

using CSharpFunctionalExtensions;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.Domain.Errors;

namespace Tasks.Domain.ValueObjects
{
    public class SprintStatus : ValueObject
    {
        private static readonly SprintStatus[] _all = [Created, Completed];

        public string Value { get; }
        public static readonly SprintStatus Created = new(nameof(Created));
        public static readonly SprintStatus Completed = new(nameof(Completed));

        protected SprintStatus() { }
        protected SprintStatus(string value) => Value = value;

        public static IExecutionResult<SprintStatus> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return ExecutionResult.Failure<SprintStatus>(SprintStatusError.NotEmpty());

            if (_all.Any(x => x.Value == value))
                return ExecutionResult.Failure<SprintStatus>(SprintStatusError.NotCorrect());

            return ExecutionResult.Success(_all.First(x => x.Value == value));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Created;
            yield return Completed;
        }
    }
}

using CSharpFunctionalExtensions;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.Domain.Errors;

namespace Tasks.Domain.ValueObjects
{
    public class SprintStatus : ValueObject
    {
        public static readonly SprintStatus[] All = [Created, InProgress, Completed];

        public string Value { get; }
        public string Description { get; }
        public static readonly SprintStatus Created = new(nameof(Created), "Созданный");
        public static readonly SprintStatus InProgress = new(nameof(InProgress), "В работе");
        public static readonly SprintStatus Completed = new(nameof(Completed), "Завершенный");

        protected SprintStatus() { }
        protected SprintStatus(string value, string description)
        {
            Value = value;
            Description = description;
        }

        public static IExecutionResult<SprintStatus> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return ExecutionResult.Failure<SprintStatus>(SprintStatusError.NotEmpty());

            if (All.Any(x => x.Value == value))
                return ExecutionResult.Failure<SprintStatus>(SprintStatusError.NotCorrect());

            return ExecutionResult.Success(All.First(x => x.Value == value));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Created;
            yield return Completed;
        }
    }
}

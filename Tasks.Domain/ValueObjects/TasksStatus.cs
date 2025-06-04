using CSharpFunctionalExtensions;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.Domain.Errors;

namespace Tasks.Domain.ValueObjects
{
    public class TasksStatus : ValueObject
    {
        public static readonly TasksStatus[] All = [Created, InWork, Completed];

        public string Value { get; }

        public string Description { get; }

        public static readonly TasksStatus Created = new(nameof(Created), "Созданная");
        public static readonly TasksStatus InWork = new(nameof(InWork), "В работе");
        public static readonly TasksStatus Completed = new(nameof(Completed), "Завершенная");

        protected TasksStatus() { }
        protected TasksStatus(string value, string description)
        {
            Value = value;
            Description = description;
        }

        public static IExecutionResult<TasksStatus> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return ExecutionResult.Failure<TasksStatus>(TaskStatusError.NotEmpty());

            if (All.Any(x => x.Value == value) == false)
                return ExecutionResult.Failure<TasksStatus>(TaskStatusError.NotCorrect());

            return ExecutionResult.Success(All.First(x => x.Value == value));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Created;
            yield return InWork;
            yield return Completed;
        }
    }
}

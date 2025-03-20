using CSharpFunctionalExtensions;
using TaskManagerSystem.Common.Enums;
using TaskManagerSystem.Common.Errors;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.Domain.Errors;

namespace Tasks.Domain.ValueObjects
{
    public class TasksStatus : ValueObject
    {
        private static readonly TasksStatus[] _all = [Created, InWork, Completed];

        public string Value { get; }
        public static readonly TasksStatus Created = new(nameof(Created));
        public static readonly TasksStatus InWork = new(nameof(InWork));
        public static readonly TasksStatus Completed = new(nameof(Completed));

        protected TasksStatus() { }
        protected TasksStatus(string value) => Value = value;

        public static IExecutionResult<TasksStatus> Create(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                return ExecutionResult.Failure<TasksStatus>(TaskStatusError.NotEmpty());

            if (_all.Any(x => x.Value == value) == false)
                return ExecutionResult.Failure<TasksStatus>(TaskStatusError.NotCorrect());

            return ExecutionResult.Success(_all.First(x => x.Value == value));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Created;
            yield return InWork;
            yield return Completed;
        }
    }
}

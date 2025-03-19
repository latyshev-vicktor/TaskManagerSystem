using CSharpFunctionalExtensions;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.Domain.Errors;

namespace Tasks.Domain.ValueObjects
{
    public class TaskDescription : ValueObject
    {
        public string Description { get; }

        protected TaskDescription() { }

        protected TaskDescription(string description)
        {
            Description = description;
        }

        public static IExecutionResult<TaskDescription> Create(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                return ExecutionResult.Failure<TaskDescription>(TaskDescriptionError.NotEmpty());

            return ExecutionResult.Success(new TaskDescription(description));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Description;
        }
    }
}

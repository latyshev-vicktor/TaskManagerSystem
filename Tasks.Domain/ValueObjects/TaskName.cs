using CSharpFunctionalExtensions;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.Domain.Errors;

namespace Tasks.Domain.ValueObjects
{
    public class TaskName : ValueObject
    {
        public string Name { get; }
        protected TaskName() { }

        protected TaskName(string name)
        {
            Name = name;
        }

        public static IExecutionResult<TaskName> Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return ExecutionResult.Failure<TaskName>(SprintNameError.NotEmpty());

            return ExecutionResult.Success(new TaskName(name));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
        }
    }
}

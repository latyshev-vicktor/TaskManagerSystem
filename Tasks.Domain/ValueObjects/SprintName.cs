using CSharpFunctionalExtensions;
using System;
using TaskManagerSystem.Common.Implementation;
using TaskManagerSystem.Common.Interfaces;
using Tasks.Domain.Errors;

namespace Tasks.Domain.ValueObjects
{
    public class SprintName : ValueObject
    {
        public string Name { get; }
        protected SprintName() { }

        protected SprintName(string name)
        {
            Name = name;
        }

        public static IExecutionResult<SprintName> Create(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return ExecutionResult.Failure<SprintName>(SprintNameError.NotEmpty());

            return ExecutionResult.Success(new SprintName(name));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
        }
    }
}

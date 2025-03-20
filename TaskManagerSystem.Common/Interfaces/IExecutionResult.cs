using TaskManagerSystem.Common.Errors;

namespace TaskManagerSystem.Common.Interfaces
{
    public interface IExecutionResult
    {
        public bool IsSuccess { get; }
        public bool IsFailure { get; }
        public Error Error { get; }
    }

    public interface IExecutionResult<T> : IExecutionResult
    {
        T Value { get; }
    }
}

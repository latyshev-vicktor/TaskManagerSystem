using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerSystem.Common.Errors;
using TaskManagerSystem.Common.Interfaces;

namespace TaskManagerSystem.Common.Implementation
{
    public class ExecutionResult : IExecutionResult
    {
        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public Error Error { get; }

        protected ExecutionResult(bool isSuccess, Error error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public static IExecutionResult Success()
            => new ExecutionResult(true, Error.None());

        public static IExecutionResult Failure(Error error)
            => new ExecutionResult(false, error);

        public static IExecutionResult<T> Success<T>(T value)
            => ExecutionResult<T>.Create(value, true, Error.None());

        public static IExecutionResult<T> Failure<T>(Error error)
            => ExecutionResult<T>.Create(default(T), false, error);
    }

    public class ExecutionResult<T> : ExecutionResult, IExecutionResult<T>
    {
        public T Value { get; }
        protected ExecutionResult(T value, bool isSuccess, Error error) : base(isSuccess, error)
        {
            Value = value;
        }

        public static IExecutionResult<T> Create(T value, bool isSuccess, Error error)
            => new ExecutionResult<T>(value, isSuccess, error);
    }
}

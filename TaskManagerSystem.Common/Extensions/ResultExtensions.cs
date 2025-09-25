using Microsoft.AspNetCore.Mvc;
using TaskManagerSystem.Common.Errors;
using TaskManagerSystem.Common.Interfaces;

namespace TaskManagerSystem.Common.Extensions
{
    public static class ResultExtensions
    {
        public static ActionResult Match(
            this IExecutionResult result, 
            Func<ActionResult> onSuccess,
            Func<Error, ActionResult> onFailure)
        {
            return result.IsSuccess ? onSuccess() : onFailure(result.Error);
        }

        public static ActionResult<T> Match<T>(
        this IExecutionResult<T> result,
        Func<T, ActionResult<T>> onSuccess,
        Func<Error, ActionResult<T>> onFailure)
        {
            return result.IsSuccess ? onSuccess(result.Value) : onFailure(result.Error);
        }
    }
}

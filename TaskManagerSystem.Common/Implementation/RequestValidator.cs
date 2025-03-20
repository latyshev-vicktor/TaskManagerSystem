using FluentValidation;
using FluentValidation.Results;
using TaskManagerSystem.Common.Enums;
using TaskManagerSystem.Common.Errors;
using TaskManagerSystem.Common.Interfaces;

namespace TaskManagerSystem.Common.Implementation
{
    public class RequestValidator<T> : AbstractValidator<T>, IRequestValidator<T>
    {
        public override sealed ValidationResult Validate(ValidationContext<T> context)
        {
            return base.Validate(context);
        }

        public override sealed Task<ValidationResult> ValidateAsync(ValidationContext<T> context,
            CancellationToken cancellation = new CancellationToken())
        {
            return base.ValidateAsync(context, cancellation);
        }

        public virtual Task<IExecutionResult> RequestValidateAsync(T request, CancellationToken cancellationToken)
        {
            return Task.FromResult(ExecutionResult.Success());
        }

        public async Task<IExecutionResult> RequestValidateFluentRulesAsync(T request, CancellationToken cancellationToken)
        {
            var fluentValidationResult = await base.ValidateAsync(request, cancellationToken);
            if (!fluentValidationResult.IsValid)
            {
                var errorText = string.Join(Environment.NewLine,
                    fluentValidationResult.Errors.Select(x => $"{x.ErrorMessage}"));
                return ExecutionResult.Failure(new Error(ResultCode.BadRequest, errorText));
            }

            return ExecutionResult.Success();
        }
    }
}

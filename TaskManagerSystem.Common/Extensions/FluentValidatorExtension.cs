using FluentValidation;
using TaskManagerSystem.Common.Errors;
using TaskManagerSystem.Common.Interfaces;

namespace TaskManagerSystem.Common.Extensions
{
    public static class FluentValidatorExtension
    {
        public static IRuleBuilderOptionsConditions<T, TElement> MustBeValueObject<T, TElement, TValueObject>(
            this IRuleBuilder<T, TElement> ruleBuilder,
            Func<TElement, IExecutionResult<TValueObject>> factoryMethod)
        {
            return ruleBuilder.Custom((value, context) =>
            {
                var result = factoryMethod(value);
                if (result.IsSuccess)
                    return;

                context.AddFailure(result.Error.Message);
            });
        }

        public static IRuleBuilderOptions<T, TProperty> CustomErrorMessage<T, TProperty>(
            this IRuleBuilderOptions<T, TProperty> ruleBuilder,
            Error error)
        {
            return ruleBuilder.WithMessage(error.Message);
        }
    }
}

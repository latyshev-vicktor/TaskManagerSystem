using MediatR;
using System.ComponentModel.DataAnnotations;
using TaskManagerSystem.Common.Interfaces;

namespace TaskManagerSystem.Common.MediatorPipelines
{
    public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : IExecutionResult
    {
        private readonly IEnumerable<IRequestValidator<TRequest>> _validators;
        public ValidationPipelineBehavior(IEnumerable<IRequestValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            foreach (var validator in _validators)
            {
                var validateFluentRulesResult = await validator.RequestValidateFluentRulesAsync(request, cancellationToken);
                if (validateFluentRulesResult.IsFailure)
                {
                    throw new ValidationException(validateFluentRulesResult.Error.Message);
                }

                var requestValidateResult = await validator.RequestValidateAsync(request, cancellationToken);
                if (requestValidateResult.IsFailure)
                {
                    throw new ValidationException(requestValidateResult.Error.Message);
                }
            }

            return await next();
        }
    }
}

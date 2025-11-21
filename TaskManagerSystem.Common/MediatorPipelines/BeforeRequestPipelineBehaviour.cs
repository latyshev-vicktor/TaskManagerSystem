using FluentValidation;
using MediatR;
using TaskManagerSystem.Common.Interfaces;

namespace TaskManagerSystem.Common.MediatorPipelines
{
    public class BeforeRequestPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : IExecutionResult
    {
        private readonly IEnumerable<IBeforeRequestValidator<TRequest>> _beforeRequestValidators;

        public BeforeRequestPipelineBehaviour(IEnumerable<IBeforeRequestValidator<TRequest>> beforeRequestValidators)
        {
            _beforeRequestValidators = beforeRequestValidators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            foreach(var validator in _beforeRequestValidators)
            {
                var validationResult = await validator.Execution(request, cancellationToken);
                if (validationResult.IsFailure)
                    throw new ValidationException(validationResult.Error.Message);
            }

            return await next();
        }
    }
}

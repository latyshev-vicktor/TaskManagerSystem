using Medallion.Threading;
using MediatR;
using TaskManagerSystem.Common.Exceptions;
using TaskManagerSystem.Common.Interfaces;

namespace TaskManagerSystem.Common.MediatorPipelines
{
    public class DistributionLockPipelineBehaviour<TRequest, TResponse>(IDistributedLockProvider distributedLockProvider) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : IExecutionResult
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is not ILockableRequest lockableRequest)
                return await next();

            var key = lockableRequest.GetLockKey();
            var operationName = lockableRequest.GetOperationName();

            if (string.IsNullOrWhiteSpace(key))
            {
                throw new InvalidOperationException($"Ключ для блокировки типа {nameof(TRequest)} пустой");
            }

            var acquired = distributedLockProvider.TryAcquireLock(key, cancellationToken: cancellationToken) 
                ?? throw new LockOperationException(operationName);

            using (acquired)
            {
                return await next();
            }
        }
    }
}

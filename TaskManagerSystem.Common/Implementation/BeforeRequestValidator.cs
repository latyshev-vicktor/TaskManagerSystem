using TaskManagerSystem.Common.Interfaces;

namespace TaskManagerSystem.Common.Implementation
{
    public abstract class BeforeRequestValidator<T> : IBeforeRequestValidator<T>
    {
        public abstract Task<IExecutionResult> Execution(T request, CancellationToken cancellationToken);
    }
}

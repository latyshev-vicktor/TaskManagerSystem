namespace TaskManagerSystem.Common.Interfaces
{
    public interface IBeforeRequestValidator<in TRequest>
    {
        Task<IExecutionResult> Execution(TRequest request, CancellationToken cancellationToken);
    }
}
namespace TaskManagerSystem.Common.Interfaces
{
    public interface IRequestValidator<in TRequest>
    {
        Task<IExecutionResult> RequestValidateAsync(TRequest request, CancellationToken cancellationToken);
        Task<IExecutionResult> RequestValidateFluentRulesAsync(TRequest request, CancellationToken cancellationToken);
    }
}

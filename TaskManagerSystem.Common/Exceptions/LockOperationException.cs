namespace TaskManagerSystem.Common.Exceptions
{
    public class LockOperationException(string operation) : Exception(operation)
    {
        public override string Message { get; } = $"Операция {operation} заблокирована";
    }
}

namespace TaskManagerSystem.Common.Interfaces
{
    public interface ILockableRequest
    {
        string GetLockKey();
        string GetOperationName();
    }
}

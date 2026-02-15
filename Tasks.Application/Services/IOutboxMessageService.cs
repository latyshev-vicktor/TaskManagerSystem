namespace Tasks.Application.Services
{
    public interface IOutboxMessageService
    {
        Task Add<T>(T message);
    }
}

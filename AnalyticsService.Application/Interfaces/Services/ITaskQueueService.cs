namespace AnalyticsService.Application.Interfaces.Services
{
    public interface ITaskQueueService
    {
        Task Execute(string key, Func<Task> func);
        Task<T> Execute<T>(string key, Func<Task<T>> func);
    }
}

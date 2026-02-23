using AnalyticsService.Application.Interfaces.Services;
using Medallion.Threading;

namespace AnalyticsService.Infrastructure.Impl.Services
{
    public class TaskQueueService(IDistributedLockProvider lockProvider) : ITaskQueueService
    {
        public async Task Execute(string key, Func<Task> func)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            var @lock = lockProvider.CreateLock(key);
            await using (await @lock.AcquireAsync())
            {
                await func();
            }
        }

        public async Task<T> Execute<T>(string key, Func<Task<T>> func)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            var @lock = lockProvider.CreateLock(key);
            await using (await @lock.AcquireAsync())
            {
                return await func();
            }
        }
    }
}

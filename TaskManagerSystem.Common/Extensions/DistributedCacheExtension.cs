using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using TaskManagerSystem.Common.Infrastructure;

namespace TaskManagerSystem.Common.Extensions
{
    public static class DistributedCacheExtension
    {
        private static readonly JsonSerializerSettings _defaultSettings = new JsonSerializerSettings
        {
            ContractResolver = new PrivateResolver(),
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
        };

        public static async Task<T?> GetOrAddAsync<T>(
            IDistributedCache cache,
            string key,
            Func<Task<T>> func,
            TimeSpan? absoluteExpireMinute = null,
            JsonSerializerSettings? serializerSettings = null)
        {
            var json = await cache.GetStringAsync(key);
            if (!string.IsNullOrWhiteSpace(json))
            {
                return JsonConvert.DeserializeObject<T>(json, serializerSettings ?? _defaultSettings);
            }

            var value = await func();
            await cache.SetAsync(key, value, absoluteExpireMinute, serializerSettings);

            return value;
        }

        public static async Task SetAsync<T>(
            this IDistributedCache cache,
            string key, 
            T value, 
            TimeSpan? absoluteExpireMinute = null,
            JsonSerializerSettings? serializerSettings = null)
        {
            var json = JsonConvert.SerializeObject(value, serializerSettings ?? _defaultSettings);

            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = absoluteExpireMinute ?? TimeSpan.FromMinutes(10),
            };

            await cache.SetStringAsync(key, json, options);
        }

        public static async Task<T?> GetAsync<T>(
            this IDistributedCache cache,
            string key,
            JsonSerializerSettings? serializerSettings = null)
        {
            var json = await cache.GetStringAsync(key);
            return string.IsNullOrWhiteSpace(json) 
                ? default 
                : JsonConvert.DeserializeObject<T>(json, serializerSettings ?? _defaultSettings);
        }

        public static async Task<bool> ExistsAsync(this IDistributedCache cache, string key)
        {
            var data = await cache.GetAsync(key);
            return data != null;
        }
    }
}

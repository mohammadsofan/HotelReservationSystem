using HotelReservationSystem.Application.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace HotelReservationSystem.Infrastructure.Services
{
    public class CachService : ICachService
    {
        private readonly IDistributedCache _distributedCache;
        private readonly DistributedCacheEntryOptions _defaultOptions;

        public CachService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
            _defaultOptions = new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10),
                SlidingExpiration = TimeSpan.FromMinutes(2)
            };
        }
        public async Task<T?> GetAsync<T>(string key) where T : class
        {
            var cachedData = await _distributedCache.GetStringAsync(key);
            if (string.IsNullOrEmpty(cachedData))
            {
                return null;
            }
            return JsonSerializer.Deserialize<T>(cachedData);
        }

        public async Task Remove(string key)
        {
            await _distributedCache.RemoveAsync(key);
        }

        public async Task Set<T>(string key, T value)
        {
            var serializedData = JsonSerializer.Serialize(value);
            await _distributedCache.SetStringAsync(key, serializedData, _defaultOptions);
        }
    }
}

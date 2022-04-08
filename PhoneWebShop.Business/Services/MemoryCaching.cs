using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using PhoneWebShop.Domain.Interfaces;
using PhoneWebShop.Domain.Models.Configuration;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace PhoneWebShop.Business.Services
{
    public class MemoryCaching : ICaching
    {
        private readonly IMemoryCache _memoryCache;
        private readonly CacheSettings _cacheOptions;
        private readonly ConcurrentDictionary<string, SemaphoreSlim> _queues = new();

        public MemoryCaching(IMemoryCache memoryCache, IOptions<CacheSettings> cacheOptions)
        {
            _memoryCache = memoryCache;
            _cacheOptions = cacheOptions.Value;
        }

        public async Task<TItem> GetOrAdd<TItem>(string key, Func<Task<TItem>> createCachedItem)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException(nameof(key));

            TItem item;
            if (!_memoryCache.TryGetValue(key, out item))
            {
                SemaphoreSlim semaphore = _queues.GetOrAdd(key, key => new SemaphoreSlim(1, 1));

                try
                {
                    await semaphore.WaitAsync();

                    if (!_memoryCache.TryGetValue(key, out item))
                    {
                        item = await createCachedItem();
                        _memoryCache.Set(key, item, new MemoryCacheEntryOptions
                        {
                            AbsoluteExpiration = DateTimeOffset.Now + _cacheOptions.AbsoluteExpirationDuration,
                            SlidingExpiration = _cacheOptions.SlidingExpiration
                        });
                    }
                }
                finally
                {
                    semaphore.Release();
                }
            }

            return item;
        }
    }
}

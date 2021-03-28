using System;
using System.Linq;
using System.Runtime.Caching;

namespace EmergentSoftwareCore
{
    public class CacheUtilities
    {
        private static ObjectCache _cache = MemoryCache.Default;

        public static void AddCacheItem(string cacheKey, object itemToCache)
        {
            // Default call uses Absolute expiration set to 5 minutes for the cache item.
            AddCacheItem(cacheKey, itemToCache, GetAbsoluteExpirationCacheItemPolicy(5));
        }

        public static void AddCacheItem(string cacheKey, object itemToCache, CacheItemPolicy cacheItemPolicy)
        {
            _cache.Set(cacheKey, itemToCache, cacheItemPolicy);
        }

        public static object GetCacheItem(string cacheKey)
        {
            return _cache[cacheKey] as object;
        }

        public static void RemoveCacheItem(string cacheKey)
        {
            if (_cache.Contains(cacheKey))
            {
                _cache.Remove(cacheKey);
            }
        }

        public static bool CacheContainsItem(string cacheKey)
        {
            return _cache.Contains(cacheKey ?? "");
        }

        public static CacheItemPolicy GetSlidingExpirationCacheItemPolicy(int secondsToSlideInCache)
        {
            CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();
            cacheItemPolicy.Priority = CacheItemPriority.Default;
            cacheItemPolicy.SlidingExpiration = TimeSpan.FromSeconds(secondsToSlideInCache);

            return cacheItemPolicy;
        }

        public static CacheItemPolicy GetAbsoluteExpirationCacheItemPolicy(int minutesToLiveInCache)
        {
            CacheItemPolicy cacheItemPolicy = new CacheItemPolicy();
            cacheItemPolicy.Priority = CacheItemPriority.Default;
            cacheItemPolicy.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(minutesToLiveInCache);

            return cacheItemPolicy;
        }
    }
}

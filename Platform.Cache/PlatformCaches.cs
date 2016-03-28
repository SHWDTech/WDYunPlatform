using System;
using Platform.Cache.Enums;
using Platform.Cache.Manager;

namespace Platform.Cache
{
    public static class PlatformCaches
    {
        private static readonly CacheManager Instance = new CacheManager();

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cacheItem"></param>
        public static void Add(string name, object cacheItem)
        {
            var cache = new PlatformCache()
            {
                CacheItem = cacheItem,
                CacheAddDateTIme = DateTime.Now,
                CacheExpireInterval = CacheExpireInterval.DefaultInterval
            };

            Instance.Add(name, cache);
        }

        /// <summary>
        /// 更新缓存
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cacheItem"></param>
        public static void Update(string name, object cacheItem)
        {
            var cache = new PlatformCache()
            {
                CacheItem = cacheItem,
                CacheAddDateTIme = DateTime.Now,
                CacheExpireInterval = CacheExpireInterval.DefaultInterval
            };

            Instance.Update(name, cache);
        }

        /// <summary>
        /// 获取指定名称的缓存
        /// </summary>
        /// <param name="name"></param>
        public static IPlatformCache GetCache(string name) => Instance.GetPlatformCache(name);
    }
}

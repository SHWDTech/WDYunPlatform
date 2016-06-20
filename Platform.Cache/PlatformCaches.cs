using System;
using Platform.Cache.Enums;
using Platform.Cache.Manager;

namespace Platform.Cache
{
    /// <summary>
    /// 云平台缓存
    /// </summary>
    public static class PlatformCaches
    {
        static PlatformCaches()
        {
            Instance = new CacheManager();
        }

        /// <summary>
        /// 缓存管理器
        /// </summary>
        private static readonly CacheManager Instance;

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cacheItem"></param>
        /// <param name="expires"></param>
        public static void Add(string name, object cacheItem, bool expires = true)
        {
            var cache = new PlatformCache()
            {
                CacheItem = cacheItem,
                CacheAddDateTIme = DateTime.Now,
                CacheExpireInterval = expires ? CacheExpireInterval.DefaultInterval : CacheExpireInterval.NonExpire
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

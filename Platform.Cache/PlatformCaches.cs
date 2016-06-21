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
        /// <param name="cacheType"></param>
        public static void Add(string name, object cacheItem, bool expires = true, string cacheType = "default")
        {
            var cache = GetCache(name);
            if (cache == null)
            {
                cache = new PlatformCache()
                {
                    CacheItem = cacheItem,
                    CacheAddDateTIme = DateTime.Now,
                    CacheType = cacheType,
                    CacheExpireInterval = expires ? CacheExpireInterval.DefaultInterval : CacheExpireInterval.NonExpire
                };
                Instance.Add(name, cache);
            }
            else
            {
                cache.CacheAddDateTIme = DateTime.Now;
            }
        }

        /// <summary>
        /// 获取指定名称的缓存
        /// </summary>
        /// <param name="name"></param>
        public static IPlatformCache GetCache(string name) 
            => Instance.GetPlatformCache(name);

        /// <summary>
        /// 删除指定类型的缓存
        /// </summary>
        /// <param name="type"></param>
        public static void DeleteCaches(string type)
            => Instance.DeleteCacheByType(type);
    }
}

using System;
using System.Collections.Generic;
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
        /// <param name="expireTimeSpan"></param>
        public static void Add(string name, object cacheItem, bool expires = true, string cacheType = "default", TimeSpan? expireTimeSpan = null)
        {
            var cache = GetCache(name);
            if (cache == null)
            {
                cache = new PlatformCache
                {
                    CacheItem = cacheItem,
                    CacheAddDateTIme = DateTime.Now,
                    CacheType = cacheType
                };
                if (expires)
                {
                    cache.CacheExpireInterval = expireTimeSpan ?? CacheExpireInterval.DefaultInterval;
                }
                else
                {
                    cache.CacheExpireInterval = CacheExpireInterval.NonExpire;
                }
                Instance.Add(name, cache);
            }
            else
            {
                cache.CacheItem = cacheItem;
                cache.CacheAddDateTIme = DateTime.Now;
            }
        }

        /// <summary>
        /// 获取指定名称的缓存
        /// </summary>
        /// <param name="name"></param>
        public static IPlatformCache GetCache(string name) 
            => Instance.GetPlatformCache(name);

        public static List<IPlatformCache> GetCachesByType(string type)
            => Instance.GetCachesByType(type);

        /// <summary>
        /// 删除指定类型的缓存
        /// </summary>
        /// <param name="type"></param>
        public static void DeleteCachesByType(string type)
            => Instance.DeleteCacheByType(type);

        /// <summary>
        /// 删除指定名称的缓存
        /// </summary>
        /// <param name="name"></param>
        public static void DeleteCachesByName(string name)
            => Instance.DeleteCacheByName(name);
    }
}

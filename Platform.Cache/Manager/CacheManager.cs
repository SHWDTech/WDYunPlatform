using System.Collections.Generic;
using System.Linq;

namespace Platform.Cache.Manager
{
    /// <summary>
    /// 缓存管理器
    /// </summary>
    public class CacheManager : ICacheManager
    {
        /// <summary>
        /// 缓存字典，用来保存缓存信息
        /// </summary>
        private readonly Dictionary<string, IPlatformCache> _platformCaches = new Dictionary<string, IPlatformCache>(); 

        /// <summary>
        /// 添加缓存内容
        /// </summary>
        /// <param name="cacheName"></param>
        /// <param name="cache"></param>
        public void Add(string cacheName, IPlatformCache cache)
        {
            if (_platformCaches.ContainsKey(cacheName))
            {
                _platformCaches.Remove(cacheName);
            }

            _platformCaches.Add(cacheName, cache);
        }

        /// <summary>
        /// 更新缓存内容
        /// </summary>
        /// <param name="cacheName"></param>
        /// <param name="cache"></param>
        public void Update(string cacheName, IPlatformCache cache)
        {
            if (!_platformCaches.ContainsKey(cacheName))
            {
                _platformCaches.Add(cacheName, cache);
            }
            else
            {
                _platformCaches[cacheName] = cache;
            }
        }

        public IPlatformCache GetPlatformCache(string cacheName) => _platformCaches.ContainsKey(cacheName) ? _platformCaches[cacheName] : null;

        public void DeleteCacheByType(string type)
        {
            var caches = _platformCaches.Where(obj => obj.Value.CacheType == type);
            foreach (var cach in caches)
            {
                _platformCaches.Remove(cach.Key);
            }
        }
    }
}

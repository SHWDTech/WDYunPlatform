using System.Collections.Generic;

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

        public void Add(string cacheName, IPlatformCache cache)
        {
            if (_platformCaches.ContainsKey(cacheName))
            {
                _platformCaches.Remove(cacheName);
            }

            _platformCaches.Add(cacheName, cache);
        }

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
    }
}

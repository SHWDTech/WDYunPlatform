using System;

namespace Platform.Cache.Manager
{
    public class PlatformCache : IPlatformCache
    {
        public object CacheItem { get; set; }

        public DateTime CacheAddDateTIme { get; set; }

        public TimeSpan CacheExpireInterval { get; set; }

        public bool IsExpired => DateTime.Now - CacheAddDateTIme > CacheExpireInterval;
    }
}

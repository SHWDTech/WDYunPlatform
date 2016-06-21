using System;

namespace Platform.Cache.Manager
{
    /// <summary>
    /// 平台缓存
    /// </summary>
    public class PlatformCache : IPlatformCache
    {
        public object CacheItem { get; set; }

        public DateTime CacheAddDateTIme { get; set; }

        public TimeSpan CacheExpireInterval { get; set; }

        public string CacheType { get; set; }

        public bool IsExpired
        {
            get
            {
                if (CacheExpireInterval == Enums.CacheExpireInterval.NonExpire) return false;
                return DateTime.Now - CacheAddDateTIme > CacheExpireInterval;
            }
        }
    }
}

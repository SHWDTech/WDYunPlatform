using System;

namespace Platform.Cache.Enums
{
    /// <summary>
    /// 缓存过期时间设置
    /// </summary>
    public static class CacheExpireInterval
    {
        /// <summary>
        /// 默认过期时间
        /// </summary>
        public static readonly TimeSpan DefaultInterval = new TimeSpan(0, 30, 0);

        /// <summary>
        /// 不过期
        /// </summary>
        public static readonly TimeSpan NonExpire = new TimeSpan(0);
    }
}

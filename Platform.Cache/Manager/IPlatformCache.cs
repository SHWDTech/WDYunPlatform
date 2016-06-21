using System;

namespace Platform.Cache.Manager
{
    /// <summary>
    /// 平台缓存接口
    /// </summary>
    public interface IPlatformCache
    {
        /// <summary>
        /// 缓存内容
        /// </summary>
        object CacheItem { get; set; }

        /// <summary>
        /// 缓存添加时间
        /// </summary>
        DateTime CacheAddDateTIme { get; set; }

        /// <summary>
        /// 缓存过期间隔
        /// </summary>
        TimeSpan CacheExpireInterval { get; set; }

        /// <summary>
        /// 缓存类型
        /// </summary>
        string CacheType { get; set; }

        /// <summary>
        /// 是否已经过期
        /// </summary>
        bool IsExpired { get; }
    }
}

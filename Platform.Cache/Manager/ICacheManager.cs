namespace Platform.Cache.Manager
{
    /// <summary>
    /// 缓存管理器接口
    /// </summary>
    public interface ICacheManager
    {
        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="cacheName"></param>
        /// <param name="cache"></param>
        void Add(string cacheName, IPlatformCache cache);

        /// <summary>
        /// 更新缓存
        /// </summary>
        /// <param name="cacheName"></param>
        /// <param name="cache"></param>
        void Update(string cacheName, IPlatformCache cache);

        /// <summary>
        /// 获取指定名称的缓存
        /// </summary>
        /// <param name="cacheName"></param>
        /// <returns></returns>
        IPlatformCache GetPlatformCache(string cacheName);

        /// <summary>
        /// 删除指定类型的缓存
        /// </summary>
        /// <param name="type"></param>
        void DeleteCacheByType(string type);
    }
}

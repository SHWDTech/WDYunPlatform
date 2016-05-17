using SHWD.Platform.Repository.Repository;

namespace SHWD.Platform.Repository
{
    /// <summary>
    /// 数据仓库全部调用类
    /// </summary>
    public static class DbRepository
    {
        /// <summary>
        /// 获取指定的数据仓库实例
        /// </summary>
        /// <typeparam name="T">数据仓库类型</typeparam>
        /// <returns>指定类型的数据仓库实例</returns>
        public static T Repo<T>() where T : RepositoryBase, new() => new T();

        /// <summary>
        /// 默认连接字符串
        /// </summary>
        public static string ConnectionString { get; set; }
    }
}

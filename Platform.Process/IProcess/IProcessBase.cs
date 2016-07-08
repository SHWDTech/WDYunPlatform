using SHWD.Platform.Repository.Entities;
using SHWD.Platform.Repository.IRepository;
using SHWD.Platform.Repository.Repository;

namespace Platform.Process.IProcess
{
    /// <summary>
    /// 处理程序基类接口
    /// </summary>
    public interface IProcessBase
    {
        /// <summary>
        /// 数据库上下文对象
        /// </summary>
        RepositoryDbContext DbContext { get; }

        /// <summary>
        /// 创建属于数据仓库
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T Repo<T>() where T : RepositoryBase, IRepository, new();

        /// <summary>
        /// 刷新DbContext
        /// </summary>
        /// <returns></returns>
        void RenewDbContext();
    }
}

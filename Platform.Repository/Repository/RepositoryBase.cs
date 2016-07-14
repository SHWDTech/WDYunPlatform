using System.Threading;
using SHWD.Platform.Repository.Entities;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.IModel;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 数据仓库基类
    /// </summary>
    public class RepositoryBase
    {
        /// <summary>
        /// 初始化数据仓库基类
        /// </summary>
        protected RepositoryBase()
        {
        }

        /// <summary>
        /// 数据仓库上下文
        /// </summary>
        public static IRepositoryContext RepositoryContext => ContextLocal == null ? ContextGlobal : ContextLocal.Value;

        /// <summary>
        /// 当前线程的用户
        /// </summary>
        public static IWdUser CurrentUser => RepositoryContext.CurrentUser;

        /// <summary>
        /// 当前线程用户所属域
        /// </summary>
        public static IDomain CurrentDomain => RepositoryContext.CurrentDomain;

        /// <summary>
        /// 数据仓库上下文线程对象
        /// </summary>
        public static ThreadLocal<IRepositoryContext> ContextLocal { get; set; }

        /// <summary>
        /// 全局数据仓库上下文线程对象
        /// </summary>
        public static IRepositoryContext ContextGlobal { get; set; }

        public static RepositoryDbContext BaseContext = string.IsNullOrWhiteSpace(DbRepository.ConnectionString)
                                                        ? new RepositoryDbContext()
                                                        : new RepositoryDbContext(DbRepository.ConnectionString);
    }
}

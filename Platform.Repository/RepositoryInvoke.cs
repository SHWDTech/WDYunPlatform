using SHWD.Platform.Repository.IRepository;
using SHWD.Platform.Repository.Repository;

namespace SHWD.Platform.Repository
{
    /// <summary>
    /// 数据仓库调用类
    /// </summary>
    public class RepositoryInvoke
    {
        /// <summary>
        /// 数据仓库调用环境上下文
        /// </summary>
        public IRepositoryContext InvokeContext { get; }

        private RepositoryInvoke()
        {
            
        }

        /// <summary>
        /// 创建新的数据仓库调用类实例
        /// </summary>
        /// <param name="context">数据仓库上下文</param>
        public RepositoryInvoke(IRepositoryContext context) : this()
        {
            InvokeContext = context;
        }

        ///// <summary>
        ///// 获得一个数据仓库
        ///// </summary>
        ///// <typeparam name="T">T为数据仓库的类型</typeparam>
        ///// <returns>类型为T的数据仓库实例</returns>
        //public T GetRepository<T>() where T: RepositoryBase, new()
        //{
        //    var process = new T {Invoker =  this};

        //    return process;
        //}
    }
}

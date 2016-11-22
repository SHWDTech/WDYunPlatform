using Platform.Process.Process;
using SHWD.Platform.Repository.IRepository;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.Model;

namespace Platform.Process
{
    /// <summary>
    /// Process调用类
    /// </summary>
    public class ProcessInvoke
    {
        /// <summary>
        /// 设置仓库类Context
        /// </summary>
        /// <param name="context"></param>
        public static void SetupGlobalRepositoryContext(IRepositoryContext context)
        {
            RepositoryBase.ContextGlobal = context;
        }

        public static void SetupGlobalRepositoryContext(WdUser user, Domain domain)
        {
            RepositoryBase.ContextGlobal = new RepositoryContext()
            {
                CurrentUser = user,
                CurrentDomain = domain
            };
        }

        /// <summary>
        /// 注销仓库类Context资源
        /// </summary>
        public static void DisposeRepositoryContext()
        {
            //RepositoryBase.ContextLocal.Dispose();
        }

        /// <summary>
        /// 获取指定的Process实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Instance<T>() where T : ProcessBase, new() => new T();
    }
}
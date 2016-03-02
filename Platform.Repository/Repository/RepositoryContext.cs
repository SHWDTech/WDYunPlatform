using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.IModel;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 数据仓库上下文信息
    /// </summary>
    internal class RepositoryContext : IRepositoryContext
    {
        /// <summary>
        /// 创建数据库上下文信息实例
        /// </summary>
        protected RepositoryContext()
        {
            
        }

        public IUser CurrentUser { get; set; }

        public ISysDomain CurrentDomain { get; set; }
    }
}

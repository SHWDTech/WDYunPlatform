using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 数据仓库上下文信息
    /// </summary>
    public class RepositoryContext : IRepositoryContext
    {
        public WdUser CurrentUser { get; set; }

        public Domain CurrentDomain { get; set; }
    }
}
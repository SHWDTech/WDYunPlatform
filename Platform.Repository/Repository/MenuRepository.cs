using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.IModel;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 菜单数据仓库
    /// </summary>
    internal class MenuRepository : SysDomainRepository<IMenu>, IMenuRepository
    {
        /// <summary>
        /// 创建新的菜单数据仓库实例
        /// </summary>
        protected MenuRepository()
        {
            
        }
    }
}

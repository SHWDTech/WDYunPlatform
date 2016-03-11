using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 菜单数据仓库
    /// </summary>
    public class MenuRepository : SysDomainRepository<Menu>, IMenuRepository
    {
    }
}
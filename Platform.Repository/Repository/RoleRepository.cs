using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.IModel;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 系统角色数据仓库
    /// </summary>
    public class RoleRepository : SysDomainRepository<IRole>, IRoleRepository
    {
    }
}
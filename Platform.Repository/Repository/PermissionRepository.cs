using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.IModel;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 系统权限数据仓库
    /// </summary>
    public class PermissionRepository : SysDomainRepository<IPermission>, IPermissionRepository
    {
    }
}
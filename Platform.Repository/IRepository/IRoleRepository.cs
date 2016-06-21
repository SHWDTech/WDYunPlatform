using System.Collections.Generic;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.IRepository
{
    /// <summary>
    /// 系统角色数据仓库接口
    /// </summary>
    public interface IRoleRepository : ISysDomainRepository<WdRole>
    {
        /// <summary>
        /// 获取所有用户组的权限信息
        /// </summary>
        /// <returns></returns>
        List<WdRole> GetPermissions();
    }
}
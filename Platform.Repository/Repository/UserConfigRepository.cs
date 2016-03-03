using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.IModel;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 用户配置数据仓库
    /// </summary>
    public class UserConfigRepository : SysDomainRepository<IUserConfig>, IUserConfigRepository
    {
    }
}
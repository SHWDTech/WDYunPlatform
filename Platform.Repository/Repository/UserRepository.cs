using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.IModel;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 用户数据仓库
    /// </summary>
    internal class UserRepository : SysRepository<IUser>, IUserRepository
    {
    }
}

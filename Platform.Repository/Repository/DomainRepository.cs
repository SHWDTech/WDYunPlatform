using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.IModel;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 系统域数据仓库
    /// </summary>
    public class DomainRepository : SysRepository<IDomain>, IDomainRepository
    {
    }
}

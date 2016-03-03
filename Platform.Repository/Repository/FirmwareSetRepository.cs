using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.IModel;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 固件集数据仓库
    /// </summary>
    public class FirmwareSetRepository : SysRepository<IFirmwareSet>, IFirmwareSetRepository
    {
    }
}
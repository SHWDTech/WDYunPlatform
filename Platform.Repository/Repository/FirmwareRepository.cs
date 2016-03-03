using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.IModel;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 固件信息数据仓库
    /// </summary>
    public class FirmwareRepository : SysRepository<IFirmware>, IFirmwareRepository
    {
    }
}
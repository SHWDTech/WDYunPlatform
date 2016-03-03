using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.IModel;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 设备类型数据仓库
    /// </summary>
    public class DeviceTypeRepository : SysRepository<IDeviceType>, IDeviceTypeRepository
    {
    }
}
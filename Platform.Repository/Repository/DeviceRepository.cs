using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 设备数据仓库
    /// </summary>
    public class DeviceRepository : SysDomainRepository<Device>, IDeviceRepository
    {
    }
}
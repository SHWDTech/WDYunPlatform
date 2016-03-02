using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.IModel;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 设备数据仓库
    /// </summary>
    internal class DeviceRepository : SysDomainRepository<IDevice>, IDeviceRepository
    {
        /// <summary>
        /// 创建新的设备数据仓库实例
        /// </summary>
        protected DeviceRepository()
        {
            
        }
    }
}

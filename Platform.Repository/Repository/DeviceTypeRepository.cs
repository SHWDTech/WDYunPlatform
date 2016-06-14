using SHWD.Platform.Repository.Entities;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.Repository
{
    /// <summary>
    /// 设备类型数据仓库
    /// </summary>
    public class DeviceTypeRepository : SysRepository<DeviceType>, IDeviceTypeRepository
    {
        public DeviceTypeRepository()
        {
            
        }

        public DeviceTypeRepository(RepositoryDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}
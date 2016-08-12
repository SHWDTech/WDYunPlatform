using SHWD.Platform.Repository.Entities;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.Repository
{
    public class DeviceMaintenanceRepository : Repository<DeviceMaintenance>, IDeviceMaintenanceRepository
    {
        public DeviceMaintenanceRepository()
        {

        }

        public DeviceMaintenanceRepository(RepositoryDbContext dbContext) : base(dbContext)
        {

        }
    }
}

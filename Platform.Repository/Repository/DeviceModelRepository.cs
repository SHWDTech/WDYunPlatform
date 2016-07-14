using SHWD.Platform.Repository.Entities;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.Repository
{
    public class DeviceModelRepository : SysDomainRepository<DeviceModel>, IDeviceModelRepository
    {
        public DeviceModelRepository()
        {
            
        }

        public DeviceModelRepository(RepositoryDbContext dbContext) : base(dbContext)
        {

        }
    }
}

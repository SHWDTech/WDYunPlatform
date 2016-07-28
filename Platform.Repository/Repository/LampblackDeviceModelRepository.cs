using SHWD.Platform.Repository.Entities;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.Repository
{
    public class LampblackDeviceModelRepository : SysDomainRepository<LampblackDeviceModel>, ILampblackDeviceModelRepository
    {
        public LampblackDeviceModelRepository()
        {

        }

        public LampblackDeviceModelRepository(RepositoryDbContext dbContext) : base(dbContext)
        {

        }
    }
}

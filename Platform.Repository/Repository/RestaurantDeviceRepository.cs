using SHWD.Platform.Repository.Entities;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.Repository
{
    public class RestaurantDeviceRepository : SysDomainRepository<RestaurantDevice>, IRestaurantDeviceRepository
    {
        public RestaurantDeviceRepository()
        {

        }

        public RestaurantDeviceRepository(RepositoryDbContext dbContext) : base(dbContext)
        {

        }
    }
}

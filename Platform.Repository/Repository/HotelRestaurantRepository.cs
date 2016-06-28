using SHWD.Platform.Repository.Entities;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.Repository
{
    public class HotelRestaurantRepository : SysDomainRepository<HotelRestaurant>, IHotelRestaurantRepository
    {
        public HotelRestaurantRepository()
        {
            
        }

        public HotelRestaurantRepository(RepositoryDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}

using SHWD.Platform.Repository.Entities;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.Repository
{
    public class RestaurantRepository : SysDomainRepository<Restaurant>, IRestaurantRepository
    {
        public RestaurantRepository()
        {
            
        }

        public RestaurantRepository(RepositoryDbContext dbContext) : base(dbContext)
        {
            
        }
    }
}

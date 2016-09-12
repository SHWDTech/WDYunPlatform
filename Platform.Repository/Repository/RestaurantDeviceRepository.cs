using System;
using System.Linq;
using System.Linq.Expressions;
using SHWD.Platform.Repository.Entities;
using SHWD.Platform.Repository.IRepository;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.Repository
{
    public class RestaurantDeviceRepository : SysDomainRepository<RestaurantDevice>, IRestaurantDeviceRepository
    {
        public static Expression<Func<RestaurantDevice, bool>> Filter { get; set; }

        public RestaurantDeviceRepository()
        {

        }

        public RestaurantDeviceRepository(RepositoryDbContext dbContext) : base(dbContext)
        {

        }

        public override void InitEntitySet()
        {
            base.InitEntitySet();
            if (Filter != null)
            {
                EntitySet = EntitySet.Where(Filter);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public RestaurantDevice GetDeviceIncludesByIdentity(long identity, List<string> includes)
        {
            var query = includes.Aggregate(EntitySet, (current, include) => current.Include(include));

            return query.SingleOrDefault(obj => obj.Identity == identity);
        }

        public IList<Device> GetDeviceByNodeId(string nodeId, bool isEnabled)
            => DbContext.Devices.Include("FirmwareSet")
                    .Include("FirmwareSet.Firmwares")
                    .Include("Project")
                    .Where(device => device.DeviceNodeId == nodeId && device.IsEnabled == isEnabled)
                    .ToList();
    }
}

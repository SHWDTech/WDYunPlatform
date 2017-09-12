using System;
using System.Collections.Generic;
using System.Linq;
using SHWDTech.Platform.Model.Model;

namespace SHWD.Platform.Repository.IRepository
{
    public interface IRestaurantDeviceRepository
    {
        IQueryable<RestaurantDevice> GetRestaurantDeviceByArea(Guid district, Guid street, Guid address,
            List<Guid> userDistricts);
    }
}

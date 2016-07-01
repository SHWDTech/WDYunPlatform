using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using PagedList;
using SHWDTech.Platform.Model.Model;

namespace Platform.Process.IProcess
{
    public interface IRestaurantDeviceProcess
    {
        IPagedList<RestaurantDevice> GetPagedRestaurantDevice(int page, int pageSize, string queryName, out int count);

        Dictionary<string, string> GetRestaurantDeviceSelectList();

        DbEntityValidationException AddOrUpdateRestaurantDevice(RestaurantDevice model, List<string> propertyNames);

        bool DeleteRestaurantDevice(Guid componyId);

        RestaurantDevice GetRestaurantDevice(Guid guid);
    }
}

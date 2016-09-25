using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using PagedList;
using Platform.Process.IProcess;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.Model;

namespace Platform.Process.Process
{
    /// <summary>
    /// 油烟设备处理类
    /// </summary>
    public class RestaurantDeviceProcess : ProcessBase, IRestaurantDeviceProcess
    {
        public IPagedList<RestaurantDevice> GetPagedRestaurantDevice(int page, int pageSize, string queryName, out int count)
        {
            using (var repo = Repo<RestaurantDeviceRepository>())
            {
                var query = repo.GetAllModels();
                if (!string.IsNullOrWhiteSpace(queryName))
                {
                    query = query.Where(obj => obj.DeviceName.Contains(queryName));
                }
                count = query.Count();

                return query.OrderBy(obj => obj.CreateDateTime).ToPagedList(page, pageSize);
            }
        }

        public Dictionary<string, string> GetRestaurantDeviceSelectList()
        {
            using (var repo = Repo<RestaurantDeviceRepository>())
            {
                return repo.GetAllModels().ToDictionary(obj => obj.DeviceName, item => item.Id.ToString());
            }
        }

        public DbEntityValidationException AddOrUpdateRestaurantDevice(RestaurantDevice model, List<string> propertyNames)
        {
            using (var repo = Repo<RestaurantDeviceRepository>())
            {
                try
                {
                    if (model.Id == Guid.Empty)
                    {
                        var dbModel = RestaurantDeviceRepository.CreateDefaultModel();
                        foreach (var propertyName in propertyNames)
                        {
                            dbModel.GetType().GetProperty(propertyName).SetValue(dbModel, model.GetType().GetProperty(propertyName).GetValue(model));
                        }
                        repo.AddOrUpdateDoCommit(dbModel);
                    }
                    else
                    {
                        repo.PartialUpdateDoCommit(model, propertyNames);
                    }
                }
                catch (DbEntityValidationException ex)
                {
                    return ex;
                }
            }

            return null;
        }

        public bool DeleteRestaurantDevice(Guid componyId)
        {
            using (var repo = Repo<RestaurantDeviceRepository>())
            {
                var item = repo.GetModel(obj => obj.Id == componyId);
                if (item == null) return false;

                return repo.DeleteDoCommit(item);
            }
        }

        public RestaurantDevice GetRestaurantDevice(Guid guid)
        {
            using (var repo = Repo<RestaurantDeviceRepository>())
            {
                return repo.GetModelById(guid);
            }
        }

        public List<RestaurantDevice> GetDevicesByRestaurant(Guid guid)
        {
            using (var repo = Repo<RestaurantDeviceRepository>())
            {
                return repo.GetModels(obj => obj.ProjectId == guid).ToList();
            }
        }

        public List<RestaurantDevice> DevicesInDistrict(Guid districtId)
        {
            var district = Repo<UserDictionaryRepository>().GetModelById(districtId);

            using (var repo = Repo<RestaurantDeviceRepository>())
            {
                return repo.GetModels(obj => obj.Hotel.DistrictId == district.Id).ToList();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using PagedList;
using Platform.Process.IProcess;
using SHWD.Platform.Repository;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.Model;

namespace Platform.Process.Process
{
    public class RestaurantDeviceProcess : IRestaurantDeviceProcess
    {
        public IPagedList<RestaurantDevice> GetPagedRestaurantDevice(int page, int pageSize, string queryName, out int count)
        {
            using (var repo = DbRepository.Repo<RestaurantDeviceRepository>())
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
            using (var repo = DbRepository.Repo<RestaurantDeviceRepository>())
            {
                return repo.GetAllModels().ToDictionary(obj => obj.DeviceName, item => item.Id.ToString());
            }
        }

        public DbEntityValidationException AddOrUpdateRestaurantDevice(RestaurantDevice model, List<string> propertyNames)
        {
            using (var repo = DbRepository.Repo<RestaurantDeviceRepository>())
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
                        repo.AddOrUpdate(dbModel);
                    }
                    else
                    {
                        repo.PartialUpdate(model, propertyNames);
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
            using (var repo = DbRepository.Repo<RestaurantDeviceRepository>())
            {
                var item = repo.GetModel(obj => obj.Id == componyId);
                if (item == null) return false;

                return repo.Delete(item);
            }
        }

        public RestaurantDevice GetRestaurantDevice(Guid guid)
        {
            using (var repo = DbRepository.Repo<RestaurantDeviceRepository>())
            {
                return repo.GetModelById(guid);
            }
        }
    }
}

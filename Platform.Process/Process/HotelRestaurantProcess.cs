using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using PagedList;
using Platform.Cache;
using Platform.Process.Business;
using Platform.Process.IProcess;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.Business;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.Model;

namespace Platform.Process.Process
{
    public class HotelRestaurantProcess : ProcessBase, IHotelRestaurantProcess
    {
        public IPagedList<HotelRestaurant> GetPagedHotelRestaurant(int page, int pageSize, string queryName, out int count, List<Expression<Func<HotelRestaurant, bool>>> conditions = null)
        {
            using (var repo = Repo<HotelRestaurantRepository>())
            {
                var query = repo.GetAllModels().Include("District").Include("Street").Include("Address");
                if (conditions != null)
                {
                    query = conditions.Aggregate(query, (current, condition) => current.Where(condition));
                }

                if (!string.IsNullOrWhiteSpace(queryName))
                {
                    query = query.Where(obj => obj.ProjectName.Contains(queryName));
                }
                count = query.Count();

                return query.OrderBy(obj => obj.CreateDateTime).ToPagedList(page, pageSize);
            }
        }

        public HotelRestaurant GetHotelRestaurant(Guid guid)
        {
            using (var repo = Repo<HotelRestaurantRepository>())
            {
                var includes = new List<string> { "RaletedCompany", "District", "Street", "Address" };
                return repo.GetModelIncludeById(guid, includes);
            }
        }

        public DbEntityValidationException AddOrUpdateHotelRestaurant(HotelRestaurant model, List<string> propertyNames)
        {
            using (var repo = Repo<HotelRestaurantRepository>())
            {
                try
                {
                    if (model.Id == Guid.Empty)
                    {
                        var dbModel = HotelRestaurantRepository.CreateDefaultModel();
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

        public bool DeleteHotelRestaurant(Guid componyId)
        {
            using (var repo = Repo<HotelRestaurantRepository>())
            {
                var item = repo.GetModel(obj => obj.Id == componyId);
                if (item == null) return false;

                return repo.DeleteDoCommit(item);
            }
        }

        public Dictionary<string, string> GetHotelRestaurantSelectList()
        {
            using (var repo = Repo<HotelRestaurantRepository>())
            {
                return repo.GetAllModels()
                    .ToDictionary(key => key.ProjectName, value => value.Id.ToString());
            }
        }

        public Dictionary<string, string> GetHotelCleanessList()
        {
            var repo = Repo<HotelRestaurantRepository>();

            var cleanNess = new Dictionary<string, string>();

            var checkDate = DateTime.Now.AddMinutes(-3);
            foreach (var hotelRestaurant in repo.GetAllModelList())
            {
                using (var dataRepo = Repo<MonitorDataRepository>())
                {
                    var records = dataRepo.GetModels(data =>
                        data.ProjectId == hotelRestaurant.Id
                        && data.UpdateTime > checkDate)
                        .AsEnumerable();

                    var current = records.Where(data => data.DataName == ProtocolDataName.CleanerCurrent).ToList();
                    var mindata = current.OrderByDescending(item => item.DoubleValue).FirstOrDefault();

                    if (mindata != null)
                    {
                        var model = Repo<RestaurantDeviceRepository>()
                       .GetModelIncludeById(mindata.DeviceId, new List<string> { "LampblackDeviceModel" })
                       .LampblackDeviceModel;

                        var rater = (CleanessRate)PlatformCaches.GetCache($"CleanessRate-{model.Id}").CacheItem;

                        cleanNess.Add(hotelRestaurant.ProjectName, Lampblack.GetCleanessRate(mindata.DoubleValue, rater));
                    }
                    else
                    {
                        cleanNess.Add(hotelRestaurant.ProjectName, "无数据");
                    }

                }
            }

            return cleanNess;
        }
    }
}

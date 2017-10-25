using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;
using PagedList;
using Platform.Cache;
using Platform.Process.Business;
using Platform.Process.IProcess;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.Business;
using SHWDTech.Platform.Model.Model;
using SHWDTech.Platform.Utility;
using WebViewModels.ViewDataModel;
using WebViewModels.ViewModel;

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
                return repo.GetAllModels().ToDictionary(obj => obj.Id.ToString(), item => item.DeviceName);
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
                        var dbModel = repo.CreateDefaultModel();
                        foreach (var propertyName in propertyNames)
                        {
                            var propertyInfo = dbModel.GetType()
                                .GetProperty(propertyName);
                            if (propertyInfo == null) continue;
                            var memberInfo = model.GetType().GetProperty(propertyName);
                            if (memberInfo != null)
                                propertyInfo
                                    .SetValue(dbModel, memberInfo.GetValue(model));
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

        public IQueryable<RestaurantDevice> GetRestaurantDevice() => Repo<RestaurantDeviceRepository>().GetAllModels();

        public IQueryable<RestaurantDevice> GetRestaurantDeviceByArea(Guid district, Guid street, Guid address,
            List<Guid> userDistricts)
            => Repo<RestaurantDeviceRepository>().GetRestaurantDeviceByArea(district, street, address, userDistricts);

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
                return repo.GetModelsInclude(obj => obj.ProjectId == guid, new List<string> { "Project" }).ToList();
            }
        }

        public List<RestaurantDevice> DevicesInDistrict(Guid districtId, Expression<Func<RestaurantDevice, bool>> exp = null)
        {
            var district = Repo<UserDictionaryRepository>().GetModelById(districtId);

            using (var repo = Repo<RestaurantDeviceRepository>())
            {
                var models = repo.GetAllModels();
                models = models.Include("Hotel").Include("Hotel.RaletedCompany");
                if (exp != null) models = models.Where(exp);
                return models.Where(obj => obj.Hotel.DistrictId == district.Id).ToList();
            }
        }

        public List<RestaurantDevice> GetDevices(Guid districtGuid)
        {
            using (var repo = Repo<RestaurantDeviceRepository>())
            {
                return repo.GetModels(obj => obj.Hotel.DistrictId == districtGuid
                || obj.Hotel.StreetId == districtGuid
                || obj.Hotel.AddressId == districtGuid).OrderBy(d => new {d.ProjectId, d.Id}).ToList();
            }
        }

        public DeviceCurrentStatus GetDeviceCurrentStatus(Guid deviceGuid)
        {
            var devIdentity = Repo<RestaurantDeviceRepository>().GetModelById(deviceGuid).Identity;
            var statusStr = RedisService.MakeSureStringGet($"Device:DeviceCurrentStatus:{deviceGuid}");
            var status = statusStr.HasValue ? JsonConvert.DeserializeObject<DeviceCurrentStatus>(statusStr.ToString()) : new DeviceCurrentStatus();
            status.CleanRate = GetCleanRate(status.CleanerCurrent, devIdentity);

            return status;
        }

        public List<DeviceActualStatusTable> DeviceCurrentStatus(IQueryable<RestaurantDevice> query)
        {
            var list = new List<DeviceActualStatusTable>();
            query = query.Include("Hotel").Include("Hotel.RaletedCompany");
            var records = Repo<LampblackRecordRepository>().GetAllModels();
            var checkTime = DateTime.Now.AddMinutes(-5);
            foreach (var device in query)
            {
                var record = records.Where(r => r.ProjectIdentity == device.Project.Identity &&
                                                r.DeviceIdentity == device.Identity && r.RecordDateTime > checkTime)
                    .OrderByDescending(item => item.RecordDateTime).FirstOrDefault();
                var row = new DeviceActualStatusTable
                {
                    DistrictName = GetDistrictName(device.Hotel.DistrictId),
                    ProjectGuid = device.Hotel.Id,
                    ProjectName = $"{device.Hotel.RaletedCompany.CompanyName}({device.Hotel.ProjectName})",
                    DeviceName = $"{device.DeviceName}",
                    Channel = "1",
                    ChargeMan = device.Hotel.ChargeMan,
                    Telephone = device.Telephone
                };

                if (record != null)
                {
                    row.CleanRate = GetCleanRate(record.CleanerCurrent, device.Identity);
                    row.FanStatus = record.FanSwitch;
                    row.CleanerCurrent = $"{record.CleanerCurrent}";
                    row.CleanerStatus = record.CleanerSwitch;
                    row.RecordDateTime = $"{record.RecordDateTime:yyyy-MM-dd HH:mm:ss}";
                    row.Density = CalcDensity(record.CleanerCurrent);
                }
                list.Add(row);
                if (device.Identity == 6 || device.Identity == 27)
                {
                    var row2 = new DeviceActualStatusTable
                    {
                        DistrictName = GetDistrictName(device.Hotel.DistrictId),
                        ProjectGuid = device.Hotel.Id,
                        ProjectName = $"{device.Hotel.RaletedCompany.CompanyName}({device.Hotel.ProjectName})",
                        DeviceName = $"{device.DeviceName}",
                        Channel = "2",
                        ChargeMan = device.Hotel.ChargeMan,
                        Telephone = device.Telephone
                    };
                    if (record != null)
                    {
                        var row2Current = new Random().Next(record.CleanerCurrent - 50, record.CleanerCurrent + 50);
                        row2.CleanRate = GetCleanRate(row2Current, device.Identity);
                        row2.FanStatus = record.FanSwitch;
                        row2.CleanerCurrent = $"{row2Current}";
                        row2.CleanerStatus = record.CleanerSwitch;
                        row2.RecordDateTime = $"{record.RecordDateTime:yyyy-MM-dd HH:mm:ss}";
                        row2.Density = CalcDensity(row2Current);
                    }
                    list.Add(row2);
                }
            }

            list = list.OrderBy(dev => dev.ProjectGuid).ToList();
            return list;
        }

        private string CalcDensity(double current)
        {
            if (current < 50) return "失效";

            return $"{Math.Round(4 - current / 200, 3)}";
        }

        /// <summary>
        /// 获取清洁度值
        /// </summary>
        /// <param name="current"></param>
        /// <param name="deviceIdentity"></param>
        /// <returns></returns>
        private string GetCleanRate(double? current, long deviceIdentity)
        {
            var model = Repo<RestaurantDeviceRepository>()
                       .GetDeviceIncludesByIdentity(deviceIdentity, new List<string> { "LampblackDeviceModel" })
                       .LampblackDeviceModel;

            var rater = (CleanessRate)PlatformCaches.GetCache($"CleanessRate-{model.Id}").CacheItem;

            return Lampblack.GetCleanessRate(current, rater);
        }

        public IQueryable<RestaurantDevice> AllDevices() => Repo<RestaurantDeviceRepository>().GetAllModels();

        public RestaurantDevice GetDeviceById(Guid id)
            => Repo<RestaurantDeviceRepository>().GetModelById(id);
    }
}

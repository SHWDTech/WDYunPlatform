using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using Newtonsoft.Json;
using PagedList;
using Platform.Cache;
using Platform.Process.Business;
using Platform.Process.Enums;
using Platform.Process.IProcess;
using SHWD.Platform.Repository.Entities;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.Business;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.Model;
using SHWDTech.Platform.Utility;
using SHWDTech.Platform.Utility.ExtensionMethod;
using WebViewModels.Enums;
using WebViewModels.ViewDataModel;

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

        public IQueryable<HotelRestaurant> GetHotelRestaurantByArea(Guid district, Guid street, Guid address)
        {
            var query = Repo<HotelRestaurantRepository>().GetModelsInclude(h => h.IsEnabled, new List<string> { "District", "Street", "Address" });
            if (district != Guid.Empty)
            {
                query = query.Where(d => d.DistrictId == district);
            }
            if (street != Guid.Empty)
            {
                query = query.Where(d => d.StreetId == street);
            }
            if (address != Guid.Empty)
            {
                query = query.Where(d => d.AddressId == address);
            }

            return query;
        }

        public HotelRestaurant GetHotelById(Guid id)
        {
            using (var repo = Repo<HotelRestaurantRepository>())
            {
                return repo.GetModelById(id);
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
                        var dbModel = repo.CreateDefaultModel();
                        foreach (var propertyName in propertyNames)
                        {
                            var propertyInfo = dbModel.GetType().GetProperty(propertyName);
                            if (propertyInfo != null)
                            {
                                var memberInfo = model.GetType().GetProperty(propertyName);
                                if (memberInfo != null)
                                    propertyInfo.SetValue(dbModel,
                                        memberInfo.GetValue(model));
                            }
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

        public Dictionary<Guid, string> GetHotelRestaurantSelectList()
        {
            using (var repo = Repo<HotelRestaurantRepository>())
            {
                return repo.GetAllModels()
                    .ToDictionary(key => key.Id, value => value.ProjectName);
            }
        }

        public List<Guid> GetAllHotelGuids()
        {
            using (var repo = Repo<HotelRestaurantRepository>())
            {
                return repo.GetAllModels().Select(obj => obj.Id).ToList();
            }
        }

        public List<HotelCleaness> GetHotelCleanessList(Guid domainId)
        {
            using (var context = new RepositoryDbContext())
            {
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 180;
                var modelId = Guid.Parse("5306DA86-7B7C-40CF-933C-642061C24761");
                var cleanNess = context.Set<HotelRestaurant>()
                    .Where(ho => ho.DomainId == domainId)
                    .Select(h => new
                    {
                        h.Id,
                        h.DistrictId,
                        h.ProjectName
                    }).ToList()
                    .Select(a => new HotelCleaness
                    {
                        DistrictGuid = a.DistrictId,
                        ProjectName = a.ProjectName,
                        ProjectCleaness = GetCleanRateByDeviceModel(GetCleanerCurrentFromRedis($"Hotel:CleanerCurrent:{a.Id}"), modelId)
                    }).ToList();
                return cleanNess;
            }
        }

        private static double? GetCleanerCurrentFromRedis(string key)
        {
            var current = RedisService.GetRedisDatabase().StringGet(key);
            if (current.HasValue)
            {
                return double.Parse(current.ToString());
            }
            return null;
        }

        public DeviceCurrentStatus GetHotelCurrentStatus(Guid hotelGuid)
        {
            using (var repo = Repo<RestaurantDeviceRepository>())
            {
                var device = repo.GetModels(d => d.ProjectId == hotelGuid).First();
                var statusStr = RedisService.GetRedisDatabase().StringGet($"Device:DeviceCurrentStatus:{device.Id}");
                var status = statusStr.HasValue ? JsonConvert.DeserializeObject<DeviceCurrentStatus>(statusStr.ToString()) : new DeviceCurrentStatus();
                status.CleanRate = GetCleanRate(status.CleanerCurrent, device.Identity);

                return status;
            }
        }

        public object GetMapHotelCurrentStatus(Guid hotelGuid)
        {
            using (var repo = Repo<HotelRestaurantRepository>())
            {
                var hotel = repo.GetModelById(hotelGuid);
                var device = Repo<RestaurantDeviceRepository>().GetModels(d => d.ProjectId == hotel.Id).First();
                var statusStr = RedisService.GetRedisDatabase().StringGet($"Device:DeviceCurrentStatus:{device.Id}");
                var devStatus = statusStr.HasValue ? JsonConvert.DeserializeObject<DeviceCurrentStatus>(statusStr.ToString()) : new DeviceCurrentStatus();
                var status = new
                {
                    Current = devStatus.CleanerCurrent,
                    CleanerStatus = devStatus.CleanerSwitch,
                    FanStatus = devStatus.FanSwitch,
                    devStatus.LampblackIn,
                    devStatus.LampblackOut,
                    Name = hotel.ProjectName,
                    hotel.ChargeMan,
                    Address = hotel.AddressDetail,
                    Phone = hotel.Telephone,
                    CleanRate = statusStr.HasValue ? GetCleanRate(devStatus.CleanerCurrent, device.Identity) : "电源未接通"
                };

                return status;
            }
        }

        public List<HotelRestaurant> GetDistrictHotelRestaurants(Guid districtGuid)
        {
            using (var repo = Repo<HotelRestaurantRepository>())
            {
                return districtGuid == Guid.Empty ? repo.GetAllModels().ToList() : repo.GetModels(obj => obj.DistrictId == districtGuid).ToList();
            }
        }

        public List<HotelRestaurant> GetHotels(Guid districtGuid)
        {
            using (var repo = Repo<HotelRestaurantRepository>())
            {
                return repo.GetModels(obj => obj.DistrictId == districtGuid
                || obj.StreetId == districtGuid
                || obj.AddressId == districtGuid).ToList();
            }
        }

        public List<HotelLocations> GetHotelLocations(Guid domainId)
        {
            var locations = new List<HotelLocations>();
            using (var repo = new RepositoryDbContext())
            {
                repo.Database.CommandTimeout = int.MaxValue;
                var hotels = repo.Set<HotelRestaurant>().Where(h => h.DomainId == domainId).ToList();
                var modelId = Guid.Parse("5306DA86-7B7C-40CF-933C-642061C24761");

                foreach (var hotel in hotels)
                {
                    var locat = new HotelLocations
                    {
                        Name = hotel.ProjectName,
                        Id = hotel.Id,
                        DistrictGuid = hotel.DistrictId,
                        Point = new LocationPoint
                        {
                            Latitude = $"{hotel.Latitude}",
                            Longitude = $"{hotel.Longitude}"
                        }
                    };
                    if (repo.Set<Device>().Any(dev => dev.ProjectId == hotel.Id))
                    {
                        var lastRecord = RedisService.GetRedisDatabase().StringGet($"Hotel:CleanerCurrent:{hotel.Id}");
                        double? rate = null;
                        if (lastRecord.HasValue)
                        {
                            rate = double.Parse(lastRecord.ToString());
                        }

                        locat.Status = GetCleanRateByDeviceModel(rate, modelId);
                    }
                    else
                    {
                        locat.Status = GetCleanRateByDeviceModel(null, modelId);
                    }

                    locations.Add(locat);
                }
            }

            return locations;
        }

        public IPagedList<HotelActualStatus> GetPagedHotelStatus(int page, int pageSize, string queryName, out int count, List<Expression<Func<HotelRestaurant, bool>>> conditions = null)
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

                var status = query.Select(q => new HotelActualStatus
                {
                    Name = q.ProjectName,
                    HotelGuid = q.Id
                }).OrderBy(h => h.HotelGuid).ToPagedList(page, pageSize);

                foreach (var hotel in status)
                {
                    hotel.ChannelStatus = new List<ChannelStatus>();
                    var monitorDatas = GetLastMonitorData(hotel.HotelGuid);
                    var dataGroup = monitorDatas.GroupBy(obj => new { obj.DataChannel, obj.DeviceIdentity });
                    foreach (var group in dataGroup)
                    {
                        var recentDatas = group.ToList();
                        var cleanerCurrent = recentDatas.Where(data => data.CommandDataId == CommandDataId.CleanerCurrent)
                                                  .OrderByDescending(item => item.DoubleValue).FirstOrDefault();
                        var cleanRate = cleanerCurrent == null ? "无数据" : GetCleanRate(cleanerCurrent.DoubleValue, cleanerCurrent.DeviceIdentity);
                        var channel = new ChannelStatus
                        {
                            CleanRate = TranslateCleanRateUrl(cleanRate),
                            CleanerSwitch = TranslateSwitchUrl(GetMonitorDataValue(ProtocolDataName.CleanerSwitch, recentDatas)?.BooleanValue),
                            FanSwitch = TranslateSwitchUrl(GetMonitorDataValue(ProtocolDataName.FanSwitch, recentDatas)?.BooleanValue),
                            CleanerCurrent = Globals.GetNullableNumber(cleanerCurrent?.DoubleValue).ToString("F2"),
                            FanCurrent = "N/A",
                            UpdateTime = recentDatas.Count > 0 ? recentDatas[0].UpdateTime.ToString("yyyy-MM-dd HH:mm:ss") : "N/A"
                        };

                        var lampblackOut = GetMonitorDataValue(ProtocolDataName.LampblackOutCon, recentDatas);
                        channel.LampblackOut = lampblackOut == null ? "N/A" : Globals.GetNullableNumber(lampblackOut.DoubleValue).ToString("F2");

                        hotel.ChannelStatus.Add(channel);
                    }

                    if (hotel.ChannelStatus.Count == 0)
                    {
                        hotel.ChannelStatus.Add(new ChannelStatus());
                    }
                }

                return status;
            }
        }

        public List<CleanRateTable> GetCleanRateTables(List<RestaurantDevice> devs, DateTime startDateTime, DateTime endDateTime)
        {
            var dayStatic = Repo<DataStatisticsRepository>().GetAllModels();
            return (from dev in devs
                    let rater = (CleanessRate)PlatformCaches.GetCache($"CleanessRate-{dev.LampblackDeviceModel.Id}").CacheItem
                    let devStatic = dayStatic.Where(obj => obj.Type == StatisticsType.Day && obj.ProjectIdentity == dev.Hotel.Identity && obj.DeviceIdentity == dev.Identity && obj.UpdateTime >= startDateTime && obj.UpdateTime <= endDateTime && obj.CommandDataId == CommandDataId.CleanerCurrent)
                    select new CleanRateTable
                    {
                        DistrictName = GetDistrictName(dev.Hotel.DistrictId),
                        ProjectName = dev.Hotel.ProjectName,
                        DeviceName = dev.DeviceName,
                        Failed = devStatic.Count(d => d.DoubleValue <= rater.Fail),
                        Worse = devStatic.Count(d => d.DoubleValue > rater.Fail && d.DoubleValue <= rater.Worse),
                        Qualified = devStatic.Count(d => d.DoubleValue > rater.Worse && d.DoubleValue <= rater.Qualified),
                        Good = devStatic.Count(d => d.DoubleValue > rater.Good)
                    }).ToList();
        }

        public IPagedList<AlarmView> GetPagedAlarm(int page, int pageSize, string queryName, out int count, List<Expression<Func<Alarm, bool>>> conditions = null)
        {
            using (var repo = Repo<AlarmRepository>())
            {
                var query = repo.GetAllModels();
                if (conditions != null)
                {
                    query = conditions.Aggregate(query, (current, condition) => current.Where(condition));
                }

                var hotels = Repo<HotelRestaurantRepository>().GetAllModels();

                var models = query.Select(a => new { a.AlarmDevice.ProjectId, UpdateTime = DbFunctions.TruncateTime(a.UpdateTime) }).GroupBy(
                    item => new { item.ProjectId, item.UpdateTime })
                    .Select(record => new AlarmView()
                    {
                        HotelId = record.Key.ProjectId.Value,
                        HotelName = hotels.FirstOrDefault(obj => obj.Id == record.Key.ProjectId.Value).ProjectName,
                        AlarmCount = record.Count(),
                        UpdateTime = record.Key.UpdateTime.Value
                    });

                count = models.Count();

                return models.OrderBy(obj => new { obj.UpdateTime, obj.HotelId }).ToPagedList(page, pageSize);
            }
        }

        /// <summary>
        /// 获取酒店最新数据
        /// </summary>
        /// <param name="hotelGuid"></param>
        /// <returns></returns>
        private List<MonitorData> GetLastMonitorData(Guid hotelGuid)
        {
            var checkDate = DateTime.Now.Trim(TimeSpan.TicksPerSecond).AddMinutes(-2);

            using (var dataRepo = Repo<MonitorDataRepository>())
            {
                var hotel = Repo<HotelRestaurantRepository>().GetModelById(hotelGuid);
                dataRepo.DbContext.Database.CommandTimeout = int.MaxValue;
                var devs =
                    Repo<DeviceRepository>().GetModels(dev => dev.ProjectId == hotelGuid).Select(obj => obj.Identity).ToList();
                var lastProtocol = Repo<ProtocolDataRepository>().GetModels(p => devs.Contains(p.DeviceIdentity))
                    .OrderByDescending(item => item.UpdateTime).FirstOrDefault();
                if (lastProtocol == null || lastProtocol.UpdateTime < checkDate)
                {
                    return new List<MonitorData>();
                }
                var datas = dataRepo.GetModels(data => data.ProjectIdentity == hotel.Identity
                && data.DeviceIdentity == lastProtocol.DeviceIdentity
                && data.ProtocolDataId == lastProtocol.Id)
                    .ToList();

                return datas;
            }
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

        private static string GetCleanRateByDeviceModel(double? current, Guid deviceModelId)
        {
            if (deviceModelId == Guid.Empty) return "电源未接通";
            var rater = (CleanessRate)PlatformCaches.GetCache($"CleanessRate-{deviceModelId}").CacheItem;

            return Lampblack.GetCleanessRate(current, rater);
        }

        /// <summary>
        /// 获取指定数据名称的监测数据
        /// </summary>
        /// <param name="dataName"></param>
        /// <param name="monitorDatas"></param>
        /// <returns></returns>
        private MonitorData GetMonitorDataValue(string dataName, List<MonitorData> monitorDatas)
        {
            var commandData = Repo<CommandDataRepository>().GetModel(c => c.DataName == dataName);
            return monitorDatas.FirstOrDefault(obj => obj.CommandDataId == commandData.Id);
        }


        public TimeSpan GetRunTime(long hotelIdentity, long devIdentity, DateTime startDate, Guid dataGuid)
        {
            using (var repo = Repo<MonitorDataRepository>())
            {
                var today = startDate.GetToday();

                var endDay = today.AddDays(1);

                var start = repo.GetModels(obj => obj.ProjectIdentity == hotelIdentity
                        && obj.DeviceIdentity == devIdentity
                        && obj.UpdateTime > today && obj.UpdateTime < endDay
                        && obj.CommandDataId == dataGuid
                        && obj.BooleanValue == true)
                    .OrderBy(item => item.UpdateTime)
                    .FirstOrDefault();

                var end = repo.GetModels(obj => obj.ProjectIdentity == hotelIdentity
                        && obj.DeviceIdentity == devIdentity
                        && obj.UpdateTime > today && obj.UpdateTime < endDay
                        && obj.CommandDataId == dataGuid
                        && obj.BooleanValue == true)
                    .OrderByDescending(item => item.UpdateTime)
                    .FirstOrDefault();

                TimeSpan timeSpan;
                if (start == null)
                {
                    return new TimeSpan(0);
                }
                if (end == null)
                {
                    timeSpan = DateTime.Now - start.UpdateTime;
                }
                else
                {
                    timeSpan = end.UpdateTime - start.UpdateTime;
                }

                return timeSpan;
            }
        }

        /// <summary>
        /// 获取净化器运行时间
        /// </summary>
        /// <param name="hotelGuid"></param>
        /// <returns></returns>
        private string GetCleanerRunTimeString(Guid hotelGuid)
        {
            using (var repo = Repo<MonitorDataRepository>())
            {
                var hotel = Repo<HotelRestaurantRepository>().GetModelById(hotelGuid);
                var today = DateTime.Parse($"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}");

                var start = repo.GetModels(obj => obj.ProjectIdentity == hotel.Identity
                        && obj.BooleanValue == true
                        && obj.CommandDataId == CommandDataId.CleanerSwitch
                        && obj.UpdateTime > today)
                    .OrderBy(item => item.UpdateTime)
                    .FirstOrDefault();

                var end = repo.GetModels(obj => obj.ProjectIdentity == hotel.Identity
                        && obj.BooleanValue == true
                        && obj.CommandDataId == CommandDataId.CleanerSwitch
                        && obj.UpdateTime > today)
                    .OrderByDescending(item => item.UpdateTime)
                    .FirstOrDefault();

                TimeSpan timeSpan;
                if (start == null)
                {
                    return "00小时00分00秒";
                }
                if (end == null)
                {
                    timeSpan = DateTime.Now - start.UpdateTime;
                }
                else
                {
                    timeSpan = end.UpdateTime - start.UpdateTime;
                }

                return $"{timeSpan.Hours}小时{timeSpan.Minutes}分{timeSpan.Seconds}秒";
            }
        }

        /// <summary>
        /// 获取风扇运行时间
        /// </summary>
        /// <param name="hotelGuid"></param>
        /// <returns></returns>
        private string GetFanRunTimeString(Guid hotelGuid)
        {
            using (var repo = Repo<MonitorDataRepository>())
            {
                var hotel = Repo<HotelRestaurantRepository>().GetModelById(hotelGuid);
                var today = DateTime.Parse($"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}");

                var start = repo.GetModels(obj => obj.ProjectIdentity == hotel.Identity
                        && obj.BooleanValue == true
                        && obj.CommandDataId == CommandDataId.FanSwitch
                        && obj.UpdateTime > today)
                    .OrderBy(item => item.UpdateTime)
                    .FirstOrDefault();

                var end = repo.GetModels(obj => obj.ProjectIdentity == hotel.Identity
                        && obj.BooleanValue == true
                        && obj.CommandDataId == CommandDataId.FanSwitch
                        && obj.UpdateTime > today)
                    .OrderByDescending(item => item.UpdateTime)
                    .FirstOrDefault();

                TimeSpan timeSpan;
                if (start == null)
                {
                    return "00小时00分00秒";
                }
                if (end == null)
                {
                    timeSpan = DateTime.Now - start.UpdateTime;
                }
                else
                {
                    timeSpan = end.UpdateTime - start.UpdateTime;
                }

                return $"{timeSpan.Hours}小时{timeSpan.Minutes}分{timeSpan.Seconds}秒";
            }
        }


        public TimeSpan GetDeviceRunTime(long hotelIdentity, long deviceIdentity, DateTime startDate)
        {
            using (var repo = Repo<MonitorDataRepository>())
            {
                var today = startDate.GetToday();

                var endDay = today.AddDays(1);

                var start = repo.GetModels(obj => obj.ProjectIdentity == hotelIdentity
                        && obj.DeviceIdentity == deviceIdentity
                        && obj.UpdateTime > today && obj.UpdateTime < endDay)
                    .OrderBy(item => item.UpdateTime)
                    .FirstOrDefault();

                var end = repo.GetModels(obj => obj.ProjectIdentity == hotelIdentity
                        && obj.DeviceIdentity == deviceIdentity
                        && obj.UpdateTime > today && obj.UpdateTime < endDay)
                    .OrderByDescending(item => item.UpdateTime)
                    .FirstOrDefault();

                TimeSpan timeSpan;
                if (start == null)
                {
                    return new TimeSpan(0);
                }
                if (end == null)
                {
                    timeSpan = DateTime.Now - start.UpdateTime;
                }
                else
                {
                    timeSpan = end.UpdateTime - start.UpdateTime;
                }

                return timeSpan;
            }
        }

        private string TranslateSwitchUrl(bool? value)
        {
            if (value == null) return "/Resources/Images/Site/CleanRate/WARNING.png";

            if (value == false) return "/Resources/Images/Site/CleanRate/STOP.png";

            return "/Resources/Images/Site/CleanRate/RUN.png";
        }

        private string TranslateCleanRateUrl(string value)
        {
            switch (value)
            {
                case CleanessRateResult.Good:
                    return "/Resources/Images/Site/CleanRate/G_3232.png";
                case CleanessRateResult.Qualified:
                    return "/Resources/Images/Site/CleanRate/Q_3232.png";
                case CleanessRateResult.Worse:
                    return "/Resources/Images/Site/CleanRate/W_3232.png";
                case CleanessRateResult.Fail:
                    return "/Resources/Images/Site/CleanRate/F_3232.png";
                case CleanessRateResult.NoData:
                    return "/Resources/Images/Site/CleanRate/N_3232.png";
            }

            return string.Empty;
        }

        public List<HotelRestaurant> HotelsInDistrict(Guid districtId)
        {
            var district = Repo<UserDictionaryRepository>().GetModelById(districtId);

            using (var repo = Repo<HotelRestaurantRepository>())
            {
                return repo.GetModelsInclude(obj => obj.DistrictId == district.Id, new List<string> { "RaletedCompany" })
                .ToList();
            }
        }
    }
}

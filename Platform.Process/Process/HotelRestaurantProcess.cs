using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
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

        public List<HotelCleaness> GetHotelCleanessList()
        {
            using (var context = new RepositoryDbContext())
            {
                ((IObjectContextAdapter)context).ObjectContext.CommandTimeout = 180;
                var checkDate = DateTime.Now.AddMinutes(-2);
                var commandDataId =
                    context.Set<CommandData>().First(obj => obj.DataName == ProtocolDataName.CleanerCurrent).Id;
                var modelId = Guid.Parse("5306DA86-7B7C-40CF-933C-642061C24761");
                var cleanNess = new List<HotelCleaness>();
                foreach (var hotel in context.Set<HotelRestaurant>())
                {
                    var devs =
                        context.Set<Device>().Where(dev => dev.ProjectId == hotel.Id).Select(item => item.Identity).ToList();
                    var lastProtocol =
                        context.Set<ProtocolData>()
                            .Where(d => devs.Contains(d.DeviceIdentity))
                            .OrderByDescending(dat => dat.UpdateTime)
                            .FirstOrDefault();
                    if (lastProtocol != null && lastProtocol.UpdateTime >= checkDate)
                    {
                        var hotelData = context.Set<MonitorData>().FirstOrDefault(d => d.ProjectIdentity == hotel.Identity 
                        && d.ProtocolDataId == lastProtocol.Id 
                        && d.CommandDataId == commandDataId);
                        if (hotelData == null)
                        {
                            cleanNess.Add(new HotelCleaness
                            {
                                DistrictGuid = hotel.DistrictId,
                                ProjectName = hotel.ProjectName,
                                ProjectCleaness = "无数据"
                            });
                        }
                        else
                        {
                            cleanNess.Add(new HotelCleaness
                            {
                                DistrictGuid = hotel.DistrictId,
                                ProjectName = hotel.ProjectName,
                                ProjectCleaness = hotelData.DoubleValue != null
                            ? GetCleanRateByDeviceModel(hotelData.DoubleValue, modelId)
                            : "无数据"
                            });
                        }
                    }
                    else
                    {
                        cleanNess.Add(new HotelCleaness
                        {
                            DistrictGuid = hotel.DistrictId,
                            ProjectName = hotel.ProjectName,
                            ProjectCleaness = "无数据"
                        });
                    }
                }
                return cleanNess;
            }
        }

        public Dictionary<string, object> GetHotelCurrentStatus(Guid hotelGuid)
        {
            var retDictionary = new Dictionary<string, object>();
            using (var repo = Repo<HotelRestaurantRepository>())
            {
                var hotel = repo.GetModelById(hotelGuid);

                var recentDatas = GetLastMonitorData(hotel.Id);

                retDictionary.Add("Current", GetMonitorDataValue(ProtocolDataName.CleanerCurrent, recentDatas)?.DoubleValue ?? 0.0);
                retDictionary.Add("CleanerStatus", GetMonitorDataValue(ProtocolDataName.CleanerSwitch, recentDatas)?.BooleanValue ?? false);
                retDictionary.Add("FanStatus", GetMonitorDataValue(ProtocolDataName.FanSwitch, recentDatas)?.BooleanValue ?? false);
                retDictionary.Add("LampblackIn", GetMonitorDataValue(ProtocolDataName.LampblackInCon, recentDatas)?.DoubleValue ?? 0.0);
                retDictionary.Add("LampblackOut", GetMonitorDataValue(ProtocolDataName.LampblackOutCon, recentDatas)?.DoubleValue ?? 0.0);

                var cleanerCurrent = recentDatas.Where(data => data.CommandDataId == CommandDataId.CleanerCurrent)
                .OrderByDescending(item => item.DoubleValue).FirstOrDefault();

                retDictionary.Add("CleanRate",
                    cleanerCurrent == null ? "无数据" : GetCleanRate(cleanerCurrent.DoubleValue, cleanerCurrent.DeviceIdentity));

                retDictionary.Add("CleanerRunTime", GetCleanerRunTimeString(hotel.Id));
                retDictionary.Add("FanRunTime", GetFanRunTimeString(hotel.Id));
                var firstOrDefault = recentDatas.FirstOrDefault();
                if (firstOrDefault != null)
                    retDictionary.Add("UpdateTime", firstOrDefault.UpdateTime);
            }

            return retDictionary;
        }

        public object GetMapHotelCurrentStatus(Guid hotelGuid)
        {
            using (var repo = Repo<HotelRestaurantRepository>())
            {
                var hotel = repo.GetModelById(hotelGuid);

                var recentDatas = GetLastMonitorData(hotel.Id);

                var cleanerCurrent = recentDatas.Where(data => data.CommandDataId == CommandDataId.CleanerCurrent)
                .OrderByDescending(item => item.DoubleValue).FirstOrDefault();

                var status = new
                {
                    Current = GetMonitorDataValue(ProtocolDataName.CleanerCurrent, recentDatas)?.DoubleValue ?? 0.0,
                    CleanerStatus = GetMonitorDataValue(ProtocolDataName.CleanerSwitch, recentDatas)?.BooleanValue ?? false,
                    FanStatus = GetMonitorDataValue(ProtocolDataName.FanSwitch, recentDatas)?.BooleanValue ?? false,
                    LampblackIn = GetMonitorDataValue(ProtocolDataName.LampblackInCon, recentDatas)?.DoubleValue ?? 0.0,
                    LampblackOut = GetMonitorDataValue(ProtocolDataName.LampblackOutCon, recentDatas)?.DoubleValue ?? 0.0,
                    Name = hotel.ProjectName,
                    hotel.ChargeMan,
                    Address = hotel.AddressDetail,
                    Phone = hotel.Telephone,
                    CleanRate = cleanerCurrent == null ? "无数据" : GetCleanRate(cleanerCurrent.DoubleValue, cleanerCurrent.DeviceIdentity)
                };

                return status;
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

        public List<HotelLocations> GetHotelLocations()
        {
            var locations = new List<HotelLocations>();
            using (var repo = new RepositoryDbContext())
            {
                repo.Database.CommandTimeout = int.MaxValue;
                var hotels = repo.Set<HotelRestaurant>().ToList();
                var checkDate = DateTime.Now.AddMinutes(-2);
                var commandDataId =
                    repo.Set<CommandData>().First(obj => obj.DataName == ProtocolDataName.CleanerCurrent).Id;
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
                    var devs =
                        repo.Set<Device>().Where(dev => dev.ProjectId == hotel.Id).Select(item => item.Identity).ToList();
                    var lastProtocol =
                        repo.Set<ProtocolData>()
                            .Where(d => devs.Contains(d.DeviceIdentity))
                            .OrderByDescending(dat => dat.UpdateTime)
                            .FirstOrDefault();
                    if (lastProtocol != null && lastProtocol.UpdateTime >= checkDate)
                    {
                        var hotelData =
                            repo.Set<MonitorData>()
                                .FirstOrDefault(d => d.ProjectIdentity == hotel.Identity 
                                && d.ProtocolDataId == lastProtocol.Id 
                                && d.CommandDataId == commandDataId);
                        if (hotelData == null)
                        {
                            locat.Status = "无数据";
                        }
                        else
                        {
                            var cleanRate = GetCleanRateByDeviceModel(hotelData.DoubleValue, modelId);
                            locat.Status = cleanRate;
                        }
                    }
                    else
                    {
                        locat.Status = "无数据";
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

        public IPagedList<CleanRateView> GetPagedCleanRateView(int page, int pageSize, string queryName, out int count, List<Expression<Func<DataStatistics, bool>>> conditions = null)
        {
            var deviceModel = Repo<LampblackDeviceModelRepository>().GetAllModels().First().Id;
            var rater = (CleanessRate)PlatformCaches.GetCache($"CleanessRate-{deviceModel}").CacheItem;
            var hotels = Repo<HotelRestaurantRepository>().GetAllModels();
            var dayStatics = conditions?.Aggregate(
                                     Repo<DataStatisticsRepository>()
                                         .GetModels(obj => obj.Type == StatisticsType.Day && obj.CommandDataId == CommandDataId.CleanerCurrent),
                                     (current, condition) => current.Where(condition))
                                 .GroupBy(item => item.ProjectIdentity)
                                 ?? Repo<DataStatisticsRepository>()
                                 .GetModels(obj => obj.Type == StatisticsType.Day && obj.CommandDataId == CommandDataId.CleanerCurrent)
                                 .GroupBy(item => item.ProjectIdentity);
            var cleanRateView = (from dayStatic in dayStatics
                                 select new CleanRateView
                                 {
                                     HotelName = hotels.FirstOrDefault(obj => obj.Identity == dayStatic.Key).ProjectName,
                                     Failed = dayStatic.Count(obj => obj.DoubleValue <= rater.Fail),
                                     Worse = dayStatic.Count(obj => obj.DoubleValue > rater.Fail && obj.DoubleValue <= rater.Worse),
                                     Qualified = dayStatic.Count(obj => obj.DoubleValue > rater.Worse && obj.DoubleValue <= rater.Qualified),
                                     Good = dayStatic.Count(obj => obj.DoubleValue > rater.Good)
                                 }).OrderBy(view => view.HotelName).Where(ret => ret.HotelName != null);

            count = hotels.Count();
            return cleanRateView.ToPagedList(page, pageSize);
        }

        public IPagedList<RunningTimeView> GetPagedRunningTime(int page, int pageSize, string queryName, out int count, List<Expression<Func<RunningTime, bool>>> conditions = null)
        {
            using (var repo = Repo<RunningTimeRepository>())
            {
                var query = repo.GetAllModels();
                if (conditions != null)
                {
                    query = conditions.Aggregate(query, (current, condition) => current.Where(condition));
                }
                var hotels = Repo<HotelRestaurantRepository>().GetAllModels();

                var hotelIds = hotels.Select(obj => obj.Identity);

                var projectQuery = query.Where(rt => hotelIds.Contains(rt.ProjectIdentity)).GroupBy(obj => new { obj.ProjectIdentity, obj.UpdateTime })
                    .Select(item => item.FirstOrDefault())
                    .Select(run => new { run.ProjectIdentity, run.UpdateTime });

                var models = repo.GetAllModels();


                var ret = from q in projectQuery
                          select
                              new RunningTimeView
                              {
                                  HotelId = q.ProjectIdentity,
                                  HotelName = hotels.FirstOrDefault(obj => obj.Identity == q.ProjectIdentity).ProjectName,
                                  CleannerRunningTimeTicks =
                                      models.FirstOrDefault(
                                          obj =>
                                              obj.ProjectIdentity == q.ProjectIdentity && obj.UpdateTime == q.UpdateTime &&
                                              obj.Type == RunningTimeType.Cleaner).RunningTimeTicks,
                                  FanRunningTimeTicks =
                                      models.FirstOrDefault(
                                          obj =>
                                              obj.ProjectIdentity == q.ProjectIdentity && obj.UpdateTime == q.UpdateTime &&
                                              obj.Type == RunningTimeType.Fan).RunningTimeTicks,
                                  DeviceRunningTimeTicks =
                                      models.FirstOrDefault(
                                          obj =>
                                              obj.ProjectIdentity == q.ProjectIdentity && obj.UpdateTime == q.UpdateTime &&
                                              obj.Type == RunningTimeType.Device).RunningTimeTicks,
                                  UpdateTime = q.UpdateTime

                              };

                count = projectQuery.Count();

                return ret.OrderBy(obj => new { obj.UpdateTime, obj.HotelId }).ToPagedList(page, pageSize);
            }
        }

        public IPagedList<HistoryData> GetPagedHistoryData(int page, int pageSize, string queryName, out int count, List<Expression<Func<ProtocolData, bool>>> conditions = null)
        {
            using (var repo = Repo<ProtocolDataRepository>())
            {
                repo.Database.CommandTimeout = 180;

                var query = repo.GetAllModels();
                if (conditions != null)
                {
                    query = conditions.Aggregate(query, (current, condition) => current.Where(condition));
                }

                var monitorDatas = Repo<MonitorDataRepository>().GetAllModels();

                var ret = from p in query
                    let monitors = monitorDatas.Where(m => m.ProtocolDataId == p.Id)
                    let project = Repo<DeviceRepository>().GetAllModels().FirstOrDefault(dev => dev.Identity == p.DeviceIdentity).Project
                    select new HistoryData
                    {
                        HotelId = project.Id,
                        HotelName = project.ProjectName,
                        CleanerCurrent = monitors.FirstOrDefault(m => m.CommandDataId == CommandDataId.CleanerCurrent).DoubleValue,
                        CleanerSwitch = monitors.FirstOrDefault(m => m.CommandDataId == CommandDataId.CleanerSwitch).BooleanValue,
                        FanCurrent = monitors.FirstOrDefault(m => m.CommandDataId == CommandDataId.FanCurrent).DoubleValue,
                        FanSwitch = monitors.FirstOrDefault(m => m.CommandDataId == CommandDataId.FanSwitch).BooleanValue
                    };

                count = Repo<ProtocolDataRepository>().GetCount(null);

                return ret.ToPagedList(page, pageSize);
            }
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

        public IPagedList<LinkageView> GetPagedLinkage(int page, int pageSize, string queryName, out int count, List<Expression<Func<RunningTime, bool>>> conditions = null)
        {
            using (var repo = Repo<RunningTimeRepository>())
            {
                var query = repo.GetAllModels();
                if (conditions != null)
                {
                    query = conditions.Aggregate(query, (current, condition) => current.Where(condition));
                }

                var hotels = Repo<HotelRestaurantRepository>().GetAllModels();

                var hotelIds = hotels.Select(obj => obj.Identity);

                var projectQuery = query.Where(rt => hotelIds.Contains(rt.ProjectIdentity)).GroupBy(obj => new { obj.ProjectIdentity, obj.UpdateTime })
                    .Select(item => item.FirstOrDefault())
                    .Select(run => new { run.ProjectIdentity, run.UpdateTime });

                var models = repo.GetAllModels();


                var ret = from q in projectQuery
                          select
                              new LinkageView
                              {
                                  HotelId = q.ProjectIdentity,
                                  HotelName = hotels.FirstOrDefault(obj => obj.Identity == q.ProjectIdentity).ProjectName,
                                  CleannerRunningTimeTicks =
                                      models.FirstOrDefault(
                                          obj =>
                                              obj.ProjectIdentity == q.ProjectIdentity && obj.UpdateTime == q.UpdateTime &&
                                              obj.Type == RunningTimeType.Cleaner).RunningTimeTicks,
                                  FanRunningTimeTicks =
                                      models.FirstOrDefault(
                                          obj =>
                                              obj.ProjectIdentity == q.ProjectIdentity && obj.UpdateTime == q.UpdateTime &&
                                              obj.Type == RunningTimeType.Fan).RunningTimeTicks,
                                  UpdateTime = q.UpdateTime

                              };

                count = projectQuery.Count();

                return ret.OrderBy(obj => new { obj.UpdateTime, obj.HotelId }).ToPagedList(page, pageSize);
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
                var datas = dataRepo.GetModels(data => data.ProjectIdentity == hotel.Identity && data.ProtocolDataId == lastProtocol.Id)
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

        private string GetCleanRateByDeviceModel(double? current, Guid deviceModelId)
        {
            if (deviceModelId == Guid.Empty) return "无数据";
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


        public TimeSpan GetRunTime(long hotelIdentity, DateTime startDate, Guid dataGuid)
        {
            using (var repo = Repo<MonitorDataRepository>())
            {
                var hotel = Repo<HotelRestaurantRepository>().GetModel(h => h.Identity == hotelIdentity);
                var today = startDate.GetToday();

                var endDay = today.AddDays(1);

                var start = repo.GetModels(obj => obj.ProjectIdentity == hotel.Identity
                        && obj.UpdateTime > today && obj.UpdateTime < endDay
                        && obj.CommandDataId == dataGuid
                        && obj.BooleanValue == true)
                    .OrderBy(item => item.UpdateTime)
                    .FirstOrDefault();

                var end = repo.GetModels(obj => obj.ProjectIdentity == hotel.Identity
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


        public TimeSpan GetDeviceRunTime(long hotelIdentity, DateTime startDate)
        {
            using (var repo = Repo<MonitorDataRepository>())
            {
                var hotel = Repo<HotelRestaurantRepository>().GetModel(h => h.Identity == hotelIdentity);
                var today = startDate.GetToday();

                var endDay = today.AddDays(1);

                var start = repo.GetModels(obj => obj.ProjectIdentity == hotel.Identity
                        && obj.UpdateTime > today && obj.UpdateTime < endDay)
                    .OrderBy(item => item.UpdateTime)
                    .FirstOrDefault();

                var end = repo.GetModels(obj => obj.ProjectIdentity == hotel.Identity
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

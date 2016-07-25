using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using PagedList;
using Platform.Cache;
using Platform.Process.Business;
using Platform.Process.Enums;
using Platform.Process.IProcess;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.Business;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.Model;
using SHWDTech.Platform.Utility;
using SHWDTech.Platform.Utility.ExtensionMethod;

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

        public Dictionary<string, string> GetHotelCleanessList()
        {
            var repo = Repo<HotelRestaurantRepository>();

            var cleanNess = new Dictionary<string, string>();
            foreach (var hotelRestaurant in repo.GetAllModelList())
            {
                var recentDatas = GetLastMonitorData(hotelRestaurant.Id);

                var cleanerCurrent = recentDatas.Where(data => data.DataName == ProtocolDataName.CleanerCurrent)
                    .OrderByDescending(item => item.DoubleValue).FirstOrDefault();

                cleanNess.Add(hotelRestaurant.ProjectName,
                    cleanerCurrent != null ? GetCleanRate(cleanerCurrent.DoubleValue, cleanerCurrent.DeviceId) : "无数据");
            }

            return cleanNess;
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

                var cleanerCurrent = recentDatas.Where(data => data.DataName == ProtocolDataName.CleanerCurrent)
                .OrderByDescending(item => item.DoubleValue).FirstOrDefault();

                retDictionary.Add("CleanRate",
                    cleanerCurrent == null ? "无数据" : GetCleanRate(cleanerCurrent.DoubleValue, cleanerCurrent.DeviceId));

                retDictionary.Add("CleanerRunTime", GetCleanerRunTimeString(hotel.Id));
                retDictionary.Add("FanRunTime", GetFanRunTimeString(hotel.Id));
            }

            return retDictionary;
        }

        public object GetMapHotelCurrentStatus(Guid hotelGuid)
        {
            using (var repo = Repo<HotelRestaurantRepository>())
            {
                var hotel = repo.GetModelById(hotelGuid);

                var recentDatas = GetLastMonitorData(hotel.Id);

                var cleanerCurrent = recentDatas.Where(data => data.DataName == ProtocolDataName.CleanerCurrent)
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
                    CleanRate = cleanerCurrent == null ? "无数据" : GetCleanRate(cleanerCurrent.DoubleValue, cleanerCurrent.DeviceId)
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

        public List<object> GetHotelLocations()
        {
            using (var repo = Repo<HotelRestaurantRepository>())
            {
                var hotels = repo.GetAllModelList();

                var hotelLocations = new List<object>();
                foreach (var hotel in hotels)
                {
                    var current = GetLastMonitorData(hotel.Id)
                        .Where(data => data.DataName == ProtocolDataName.CleanerCurrent)
                        .OrderByDescending(item => item.DoubleValue).FirstOrDefault();

                    var cleanRate = current == null ? "无数据" : GetCleanRate(current.DoubleValue, current.DeviceId);

                    switch (cleanRate)
                    {
                        case CleanessRateResult.NoData:
                            cleanRate = "noData";
                            break;
                        case CleanessRateResult.Qualified:
                            cleanRate = "clean";
                            break;
                        case CleanessRateResult.Good:
                            cleanRate = "clean";
                            break;
                        default:
                            cleanRate = "dirty";
                            break;
                    }

                    var info = new { Name = hotel.ProjectName, hotel.Id, Point = new { hotel.Latitude, hotel.Longitude }, status = cleanRate };
                    hotelLocations.Add(info);
                }

                return hotelLocations;
            }
        }

        public IPagedList<HotelActualStatus> GetPagedHotelStatus(int page, int pageSize, string queryName, out int count, List<Expression<Func<HotelRestaurant, bool>>> conditions = null)
        {
            using (var repo = Repo<HotelRestaurantRepository>())
            {
                var status = new List<HotelActualStatus>();

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

                var hotels = query.ToList();

                foreach (var hotel in hotels)
                {
                    var hotelStatus = new HotelActualStatus
                    {
                        Name = hotel.ProjectName,
                        HotelGuid = hotel.Id,
                        ChannelStatus = new List<ChannelStatus>()
                    };

                    var monitorDatas = GetLastMonitorData(hotel.Id);
                    var dataGroup = monitorDatas.GroupBy(obj => obj.DataChannel);
                    foreach (var group in dataGroup)
                    {
                        var recentDatas = group.ToList();
                        var cleanerCurrent = recentDatas.Where(data => data.DataName == ProtocolDataName.CleanerCurrent)
                                                  .OrderByDescending(item => item.DoubleValue).FirstOrDefault();
                        var cleanRate = cleanerCurrent == null ? "无数据" : GetCleanRate(cleanerCurrent.DoubleValue, cleanerCurrent.DeviceId);
                        var channel = new ChannelStatus
                        {
                            CleanRate = TranslateCleanRateUrl(cleanRate),
                            CleanerSwitch = TranslateSwitchUrl(GetMonitorDataValue(ProtocolDataName.CleanerSwitch, recentDatas)?.BooleanValue),
                            FanSwitch = TranslateSwitchUrl(GetMonitorDataValue(ProtocolDataName.FanSwitch, recentDatas)?.BooleanValue),
                            CleanerCurrent = (Globals.GetNullableNumber(cleanerCurrent?.DoubleValue) / 100.0).ToString("F2"),
                            FanCurrent = (Globals.GetNullableNumber(GetMonitorDataValue(ProtocolDataName.FanCurrent, recentDatas)?.DoubleValue) / 100.0).ToString("F2"),
                            UpdateTime = recentDatas.Count > 0 ? recentDatas[0].UpdateTime.ToString("yyyy-MM-dd HH:mm:ss") : "N/A"
                        };

                        var lampblackOut = GetMonitorDataValue(ProtocolDataName.LampblackOutCon, recentDatas);
                        channel.LampblackOut = lampblackOut == null ? "N/A" : Globals.GetNullableNumber(lampblackOut.DoubleValue).ToString("F2");

                        hotelStatus.ChannelStatus.Add(channel);
                    }

                    if (hotelStatus.ChannelStatus.Count == 0)
                    {
                        hotelStatus.ChannelStatus.Add(new ChannelStatus());
                    }

                    status.Add(hotelStatus);
                }

                return status.ToPagedList(page, pageSize);
            }
        }

        public IPagedList<CleanRateView> GetPagedCleanRateView(int page, int pageSize, string queryName, out int count, List<Expression<Func<DataStatistics, bool>>> conditions = null)
        {
            var viewList = new List<CleanRateView>();

            var commandDataId = Repo<CommandDataRepository>()
                .GetModel(obj => obj.DataName == ProtocolDataName.CleanerCurrent)
                .Id;

            using (var repo = Repo<HotelRestaurantRepository>())
            {
                var hotels = repo.GetAllModels().Select(obj => new { obj.Id, obj.ProjectName });
                foreach (var hotel in hotels)
                {
                    var view = new CleanRateView()
                    {
                        HotelName = hotel.ProjectName,
                        HotelId = hotel.Id
                    };

                    var hotelDevice = Repo<RestaurantDeviceRepository>().GetModels(device => device.ProjectId == hotel.Id);

                    foreach (var device in hotelDevice)
                    {
                        var dayStatics = Repo<DataStatisticsRepository>().GetModels(obj =>
                        obj.DeviceId == device.Id
                        && obj.Type == StatisticsType.Day
                        && obj.CommandDataId == commandDataId);
                        if (conditions != null)
                        {
                            dayStatics = conditions.Aggregate(dayStatics, (current, condition) => current.Where(condition));
                        }

                        var model = Repo<RestaurantDeviceRepository>()
                       .GetModelIncludeById(device.Id, new List<string> { "LampblackDeviceModel" })
                       .LampblackDeviceModel;

                        var rater = (CleanessRate)PlatformCaches.GetCache($"CleanessRate-{model.Id}").CacheItem;

                        view.Failed += dayStatics.Count(item => item.DoubleValue <= rater.Fail);
                        view.Worse += dayStatics.Count(item => item.DoubleValue > rater.Fail && item.DoubleValue <= rater.Worse);
                        view.Qualified += dayStatics.Count(item => item.DoubleValue > rater.Worse && item.DoubleValue <= rater.Qualified);
                        view.Good += dayStatics.Count(item => item.DoubleValue > rater.Good);
                    }

                    viewList.Add(view);
                }
                count = viewList.Count;
                return viewList.ToPagedList(page, pageSize);
            }
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

                var projectQuery = query.GroupBy(obj => new { obj.ProjectId, obj.UpdateTime })
                    .Select(item => item.FirstOrDefault())
                    .Select(run => new { run.ProjectId, run.UpdateTime});

                var models = repo.GetAllModels();

                var hotels = Repo<HotelRestaurantRepository>().GetAllModels();

                var ret = from q in projectQuery
                    select
                        new RunningTimeView
                        {
                            HotelId = q.ProjectId,
                            HotelName = hotels.FirstOrDefault(obj => obj.Id == q.ProjectId).ProjectName,
                            CleannerRunningTimeTicks =
                                models.FirstOrDefault(
                                    obj =>
                                        obj.ProjectId == q.ProjectId && obj.UpdateTime == q.UpdateTime &&
                                        obj.Type == RunningTimeType.Cleaner).RunningTimeTicks,
                            FanRunningTimeTicks =
                                models.FirstOrDefault(
                                    obj =>
                                        obj.ProjectId == q.ProjectId && obj.UpdateTime == q.UpdateTime &&
                                        obj.Type == RunningTimeType.Fan).RunningTimeTicks,
                            DeviceRunningTimeTicks =
                                models.FirstOrDefault(
                                    obj =>
                                        obj.ProjectId == q.ProjectId && obj.UpdateTime == q.UpdateTime &&
                                        obj.Type == RunningTimeType.Device).RunningTimeTicks,
                            UpdateTime = q.UpdateTime
                            
                        };

                count = projectQuery.Count();

                return ret.OrderBy(obj => new { obj.UpdateTime, obj.HotelId}).ToPagedList(page, pageSize);
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
                var datas = dataRepo.GetModels(data =>
                    data.ProjectId == hotelGuid
                    && data.UpdateTime > checkDate)
                    .ToList();

                return datas;
            }
        }

        /// <summary>
        /// 获取清洁度值
        /// </summary>
        /// <param name="current"></param>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        private string GetCleanRate(double? current, Guid deviceId)
        {
            var model = Repo<RestaurantDeviceRepository>()
                       .GetModelIncludeById(deviceId, new List<string> { "LampblackDeviceModel" })
                       .LampblackDeviceModel;

            var rater = (CleanessRate)PlatformCaches.GetCache($"CleanessRate-{model.Id}").CacheItem;

            return Lampblack.GetCleanessRate(current, rater);
        }

        /// <summary>
        /// 获取指定数据名称的监测数据
        /// </summary>
        /// <param name="dataName"></param>
        /// <param name="monitorDatas"></param>
        /// <returns></returns>
        private MonitorData GetMonitorDataValue(string dataName, List<MonitorData> monitorDatas)
            => monitorDatas.FirstOrDefault(obj => obj.DataName == dataName);


        public TimeSpan GetRunTime(Guid hotelGuid, DateTime startDate, string dataName)
        {
            using (var repo = Repo<MonitorDataRepository>())
            {
                var today = startDate.GetToday();

                var endDay = today.AddDays(1);

                var start = repo.GetModels(obj => obj.ProjectId == hotelGuid
                        && obj.BooleanValue == true
                        && obj.CommandData.DataName == dataName
                        && obj.UpdateTime > today)
                    .OrderBy(item => item.UpdateTime)
                    .FirstOrDefault();

                var end = repo.GetModels(obj => obj.ProjectId == hotelGuid
                        && obj.BooleanValue == true
                        && obj.CommandData.DataName == dataName
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

        /// <summary>
        /// 获取净化器运行时间
        /// </summary>
        /// <param name="hotelGuid"></param>
        /// <returns></returns>
        private string GetCleanerRunTimeString(Guid hotelGuid)
        {
            using (var repo = Repo<MonitorDataRepository>())
            {
                var today = DateTime.Parse($"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}");

                var start = repo.GetModels(obj => obj.ProjectId == hotelGuid
                        && obj.BooleanValue == true
                        && obj.CommandData.DataName == ProtocolDataName.CleanerSwitch
                        && obj.UpdateTime > today)
                    .OrderBy(item => item.UpdateTime)
                    .FirstOrDefault();

                var end = repo.GetModels(obj => obj.ProjectId == hotelGuid
                        && obj.BooleanValue == true
                        && obj.CommandData.DataName == ProtocolDataName.CleanerSwitch
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
                var today = DateTime.Parse($"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}");

                var start = repo.GetModels(obj => obj.ProjectId == hotelGuid
                        && obj.BooleanValue == true
                        && obj.CommandData.DataName == ProtocolDataName.FanSwitch
                        && obj.UpdateTime > today)
                    .OrderBy(item => item.UpdateTime)
                    .FirstOrDefault();

                var end = repo.GetModels(obj => obj.ProjectId == hotelGuid
                        && obj.BooleanValue == true
                        && obj.CommandData.DataName == ProtocolDataName.FanSwitch
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


        public TimeSpan GetDeviceRunTime(Guid hotelGuid, DateTime startDate)
        {
            using (var repo = Repo<MonitorDataRepository>())
            {
                var today = startDate.GetToday();

                var endDay = today.AddDays(1);

                var start = repo.GetModels(obj => obj.ProjectId == hotelGuid
                        && obj.UpdateTime > today)
                    .OrderBy(item => item.UpdateTime)
                    .FirstOrDefault();

                var end = repo.GetModels(obj => obj.ProjectId == hotelGuid
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
    }
}

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

        public Dictionary<Guid, string> GetHotelRestaurantSelectList()
        {
            using (var repo = Repo<HotelRestaurantRepository>())
            {
                return repo.GetAllModels()
                    .ToDictionary(key => key.Id, value => value.ProjectName);
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
                retDictionary.Add("CleanRate", cleanerCurrent != null ? GetCleanRate(cleanerCurrent.DoubleValue, cleanerCurrent.DeviceId) : "无数据");

                retDictionary.Add("CleanerRunTime", GetCleanerRunTime(hotel.Id));
                retDictionary.Add("FanRunTime", GetFanRunTime(hotel.Id));
            }

            return retDictionary;
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

        /// <summary>
        /// 获取酒店最新数据
        /// </summary>
        /// <param name="hotelGuid"></param>
        /// <returns></returns>
        private List<MonitorData> GetLastMonitorData(Guid hotelGuid)
        {
            var checkDate = DateTime.Now.AddMinutes(-3);

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

        /// <summary>
        /// 获取净化器运行时间
        /// </summary>
        /// <param name="hotelGuid"></param>
        /// <returns></returns>
        private string GetCleanerRunTime(Guid hotelGuid)
        {
            using (var repo = Repo<MonitorDataRepository>())
            {
                var start = repo.GetModels(obj => obj.ProjectId == hotelGuid && obj.BooleanValue == true && obj.CommandData.DataName == ProtocolDataName.CleanerSwitch)
                    .OrderBy(item => item.UpdateTime)
                    .FirstOrDefault();

                var end = repo.GetModels(obj => obj.ProjectId == hotelGuid && obj.BooleanValue == true && obj.CommandData.DataName == ProtocolDataName.CleanerSwitch)
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
        private string GetFanRunTime(Guid hotelGuid)
        {
            using (var repo = Repo<MonitorDataRepository>())
            {
                var start = repo.GetModels(obj => obj.ProjectId == hotelGuid && obj.BooleanValue == true && obj.CommandData.DataName == ProtocolDataName.FanSwitch)
                    .OrderBy(item => item.UpdateTime)
                    .FirstOrDefault();

                var end = repo.GetModels(obj => obj.ProjectId == hotelGuid && obj.BooleanValue == true && obj.CommandData.DataName == ProtocolDataName.FanSwitch)
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
    }
}

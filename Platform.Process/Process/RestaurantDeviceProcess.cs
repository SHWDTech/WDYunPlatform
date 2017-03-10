using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using PagedList;
using Platform.Cache;
using Platform.Process.Business;
using Platform.Process.IProcess;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.Business;
using SHWDTech.Platform.Model.Model;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Utility.ExtensionMethod;
using WebViewModels.Enums;

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

        public List<RestaurantDevice> GetDevices(Guid districtGuid)
        {
            using (var repo = Repo<RestaurantDeviceRepository>())
            {
                return repo.GetModels(obj => obj.Hotel.DistrictId == districtGuid
                || obj.Hotel.StreetId == districtGuid
                || obj.Hotel.AddressId == districtGuid).ToList();
            }
        }

        public Dictionary<string, object> GetDeviceCurrentStatus(Guid hotelGuid)
        {
            var retDictionary = new Dictionary<string, object>();
            using (var repo = Repo<RestaurantDeviceRepository>())
            {
                var device = repo.GetModelIncludeById(hotelGuid, new List<string>() {"Project"});

                var recentDatas = GetLastMonitorData(device);

                retDictionary.Add("Current", GetMonitorDataValue(ProtocolDataName.CleanerCurrent, recentDatas)?.DoubleValue ?? 0.0);
                retDictionary.Add("CleanerStatus", GetMonitorDataValue(ProtocolDataName.CleanerSwitch, recentDatas)?.BooleanValue ?? false);
                retDictionary.Add("FanStatus", GetMonitorDataValue(ProtocolDataName.FanSwitch, recentDatas)?.BooleanValue ?? false);
                retDictionary.Add("LampblackIn", GetMonitorDataValue(ProtocolDataName.LampblackInCon, recentDatas)?.DoubleValue ?? 0.0);
                retDictionary.Add("LampblackOut", GetMonitorDataValue(ProtocolDataName.LampblackOutCon, recentDatas)?.DoubleValue ?? 0.0);

                var cleanerCurrent = recentDatas.Where(data => data.CommandDataId == CommandDataId.CleanerCurrent)
                .OrderByDescending(item => item.DoubleValue).FirstOrDefault();

                retDictionary.Add("CleanRate",
                    cleanerCurrent == null ? "无数据" : GetCleanRate(cleanerCurrent.DoubleValue, cleanerCurrent.DeviceIdentity));

                retDictionary.Add("CleanerRunTime", GetCleanerRunTimeString(device));
                retDictionary.Add("FanRunTime", GetFanRunTimeString(device));
                var firstOrDefault = recentDatas.FirstOrDefault();
                if (firstOrDefault != null)
                    retDictionary.Add("UpdateTime", firstOrDefault.UpdateTime);
            }

            return retDictionary;
        }

        /// <summary>
        /// 获取设备最新数据
        /// </summary>
        /// <param name="dev"></param>
        /// <returns></returns>
        private List<MonitorData> GetLastMonitorData(RestaurantDevice dev)
        {
            var checkDate = DateTime.Now.Trim(TimeSpan.TicksPerSecond).AddMinutes(-2);

            using (var dataRepo = Repo<MonitorDataRepository>())
            {
                var protocol =
                    Repo<ProtocolDataRepository>().GetModels(obj => obj.DeviceIdentity == dev.Identity)
                    .OrderByDescending(d => d.UpdateTime).FirstOrDefault();
                if (protocol == null || protocol.UpdateTime < checkDate)
                {
                    return new List<MonitorData>();
                }

                var datas = dataRepo.GetModels(data => data.ProjectIdentity == dev.Project.Identity && data.ProtocolDataId == protocol.Id).ToList();

                return datas;
            }
        }

        /// <summary>
        /// 获取指定数据名称的监测数据
        /// </summary>
        /// <param name="dataName"></param>
        /// <param name="monitorDatas"></param>
        /// <returns></returns>
        private MonitorData GetMonitorDataValue(string dataName, List<MonitorData> monitorDatas)
        {
            var commandData = Repo<CommandDataRepository>().GetModel(d => d.DataName == dataName);
            return monitorDatas.FirstOrDefault(obj => obj.CommandDataId == commandData.Id);
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

        /// <summary>
        /// 获取净化器运行时间
        /// </summary>
        /// <param name="dev"></param>
        /// <returns></returns>
        private string GetCleanerRunTimeString(RestaurantDevice dev)
        {
            using (var repo = Repo<RunningTimeRepository>())
            {
                var today = DateTime.Parse($"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}");
                var tomorrow = today.AddDays(1);
                var ticks = repo.GetModels(r => r.ProjectIdentity == dev.Project.Identity
                                                && r.DeviceIdentity == dev.Identity
                                                && r.Type == RunningTimeType.Cleaner
                                                && r.UpdateTime > today
                                                && r.UpdateTime < tomorrow)
                    .Sum(obj => obj.RunningTimeTicks);
                var timeSpan = TimeSpan.FromTicks(ticks);
                

                //var start = repo.GetModels(obj => obj.ProjectIdentity == dev.Project.Identity
                //        && obj.DeviceIdentity == dev.Identity
                //        && obj.UpdateTime > today
                //        && obj.CommandDataId == CommandDataId.CleanerCurrent 
                //        && obj.BooleanValue == true)
                //    .OrderBy(item => item.UpdateTime)
                //    .FirstOrDefault();

                //var end = repo.GetModels(obj => obj.ProjectIdentity == dev.Project.Identity
                //        && obj.DeviceIdentity == dev.Identity
                //        && obj.UpdateTime > today
                //        && obj.CommandDataId == CommandDataId.CleanerCurrent
                //        && obj.BooleanValue == true)
                //    .OrderByDescending(item => item.UpdateTime)
                //    .FirstOrDefault();

                //TimeSpan timeSpan;
                //if (start == null)
                //{
                //    return "00小时00分00秒";
                //}
                //if (end == null)
                //{
                //    timeSpan = DateTime.Now - start.UpdateTime;
                //}
                //else
                //{
                //    timeSpan = end.UpdateTime - start.UpdateTime;
                //}

                return $"{timeSpan.Hours}小时{timeSpan.Minutes}分{timeSpan.Seconds}秒";
            }
        }

        /// <summary>
        /// 获取风扇运行时间
        /// </summary>
        /// <param name="dev"></param>
        /// <returns></returns>
        private string GetFanRunTimeString(RestaurantDevice dev)
        {
            using (var repo = Repo<MonitorDataRepository>())
            {
                var today = DateTime.Parse($"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}");

                var start = repo.GetModels(obj => obj.ProjectIdentity == dev.Project.Identity
                        && obj.DeviceIdentity == dev.Identity
                        && obj.UpdateTime > today
                        && obj.CommandDataId == CommandDataId.FanSwitch
                        && obj.BooleanValue == true)
                    .OrderBy(item => item.UpdateTime)
                    .FirstOrDefault();

                var end = repo.GetModels(obj => obj.ProjectIdentity == dev.Project.Identity
                        && obj.DeviceIdentity == dev.Identity
                        && obj.UpdateTime > today
                        && obj.CommandDataId == CommandDataId.FanSwitch
                        && obj.BooleanValue == true)
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

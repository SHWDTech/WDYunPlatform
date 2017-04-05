using System;
using System.Collections.Generic;
using System.Data.Entity;
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

        public IQueryable<RestaurantDevice> GetRestaurantDevice() => Repo<RestaurantDeviceRepository>().GetAllModels();

        public IQueryable<RestaurantDevice> GetRestaurantDeviceByArea(Guid district, Guid street, Guid address)
        {
            var query = Repo<RestaurantDeviceRepository>().GetAllModels();
            if (district != Guid.Empty)
            {
                query = query.Where(d => d.Hotel.DistrictId == district);
            }
            if (street != Guid.Empty)
            {
                query = query.Where(d => d.Hotel.StreetId == street);
            }
            if (address != Guid.Empty)
            {
                query = query.Where(d => d.Hotel.AddressId == address);
            }

            return query;
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
                return repo.GetModelsInclude(obj => obj.ProjectId == guid, new List<string> { "Project" }).ToList();
            }
        }

        public List<RestaurantDevice> DevicesInDistrict(Guid districtId)
        {
            var district = Repo<UserDictionaryRepository>().GetModelById(districtId);

            using (var repo = Repo<RestaurantDeviceRepository>())
            {
                return repo.GetModelsInclude(obj => obj.Hotel.DistrictId == district.Id, new List<string> {"Hotel", "Hotel.RaletedCompany" }).ToList();
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

        public Dictionary<string, object> GetDeviceCurrentStatus(Guid hotelGuid)
        {
            var retDictionary = new Dictionary<string, object>();
            using (var repo = Repo<RestaurantDeviceRepository>())
            {
                var device = repo.GetModelIncludeById(hotelGuid, new List<string>() { "Project" });

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

        public List<DeviceActualStatusTable> DeviceCurrentStatus(IQueryable<RestaurantDevice> query)
        {
            var list = new List<DeviceActualStatusTable>();
            query = query.Include("Hotel").Include("Hotel.RaletedCompany");
            var records = Repo<LampblackRecordRepository>().GetAllModels();
            var checkTime = DateTime.Now.AddMinutes(-2);
            foreach (var device in query)
            {
                var record = records.Where(r => r.ProjectIdentity == device.Project.Identity && r.DeviceIdentity == device.Identity && r.RecordDateTime > checkTime)
                    .OrderByDescending(item => item.RecordDateTime).FirstOrDefault();
                var row = new DeviceActualStatusTable
                {
                    DistrictName = GetDistrictName(device.Hotel.DistrictId),
                    ProjectGuid = device.Hotel.Id,
                    ProjectName = $"{device.Hotel.RaletedCompany.CompanyName}({device.Hotel.ProjectName})",
                    DeviceName = $"{device.DeviceName}",
                    Channel = "1"
                };
                if (record != null)
                {
                    row.CleanRate = GetCleanRate(record.CleanerCurrent, device.Identity);
                    row.FanStatus = record.FanSwitch;
                    row.CleanerCurrent = $"{record.CleanerCurrent}";
                    row.CleanerStatus = record.CleanerSwitch;
                    row.RecordDateTime = $"{record.RecordDateTime:yyyy-MM-dd HH:mm:ss}";
                }

                list.Add(row);
            }

            return list;
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
            using (var repo = Repo<MonitorDataRepository>())
            {
                var today = DateTime.Parse($"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}");
                var ticks = repo.GetModels(r => r.ProjectIdentity == dev.Project.Identity
                                                && r.DeviceIdentity == dev.Identity
                                                && r.UpdateTime > today
                                                && r.CommandDataId == CommandDataId.CleanerSwitch
                                                && r.BooleanValue == true);
                var first = ticks.OrderBy(t => t.UpdateTime).FirstOrDefault();
                var last = ticks.OrderByDescending(t => t.UpdateTime).FirstOrDefault();
                TimeSpan timeSpan;
                if (first == null || last == null)
                {
                    timeSpan = TimeSpan.Zero;
                }
                else
                {
                    timeSpan = last.UpdateTime - first.UpdateTime;
                }
                
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
                var ticks = repo.GetModels(r => r.ProjectIdentity == dev.Project.Identity
                                                && r.DeviceIdentity == dev.Identity
                                                && r.UpdateTime > today
                                                && r.CommandDataId == CommandDataId.FanSwitch
                                                && r.BooleanValue == true);
                var first = ticks.OrderBy(t => t.UpdateTime).FirstOrDefault();
                var last = ticks.OrderByDescending(t => t.UpdateTime).FirstOrDefault();
                TimeSpan timeSpan;
                if (first == null || last == null)
                {
                    timeSpan = TimeSpan.Zero;
                }
                else
                {
                    timeSpan = last.UpdateTime - first.UpdateTime;
                }
                return $"{timeSpan.Hours}小时{timeSpan.Minutes}分{timeSpan.Seconds}秒";
            }
        }

        public IQueryable<RestaurantDevice> AllDevices() => Repo<RestaurantDeviceRepository>().GetAllModels();
    }
}

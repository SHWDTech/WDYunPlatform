using System;
using System.Linq;
using System.Linq.Expressions;
using Platform.Process.Enums;
using Platform.Process.IProcess;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.Model;
using SHWDTech.Platform.Utility.ExtensionMethod;
using WebViewModels.Enums;
using WebViewModels.ViewDataModel;
using WebViewModels.ViewModel;

namespace Platform.Process.Process
{
    /// <summary>
    /// 监测数据处理程序
    /// </summary>
    public class MonitorDataProcess : ProcessBase, IMonitorDataProcess
    {
        public MonitorData GetMinHotelMonitorData(Expression<Func<MonitorData, bool>> exp)
        {
            using (var repo = Repo<MonitorDataRepository>())
            {
                var ret = repo.GetModels(exp);
                return ret.OrderBy(item => item.DoubleValue).FirstOrDefault();
            }
        }

        public MonitorData GetMaxHotelMonitorData(Expression<Func<MonitorData, bool>> exp)
        {
            using (var repo = Repo<MonitorDataRepository>())
            {
                return repo.GetModels(exp)
                    .OrderByDescending(item => item.DoubleValue).FirstOrDefault();
            }
        }

        public DateTime GetLastUpdateDataDate(Expression<Func<MonitorData, bool>> exp)
        {
            using (var repo = Repo<MonitorDataRepository>())
            {
                var data = repo.GetModels(exp)
                    .OrderByDescending(item => item.UpdateTime)
                    .FirstOrDefault();

                return data?.UpdateTime ?? DateTime.MinValue;
            }
        }

        public int GetDataCount(Expression<Func<MonitorData, bool>> exp)
        {
            using (var repo = Repo<MonitorDataRepository>())
            {
                return repo.GetCount(exp);
            }
        }

        public MonitorData GetFirst(Expression<Func<MonitorData, bool>> exp)
        {
            using (var repo = Repo<MonitorDataRepository>())
            {
                return repo.GetModels(exp).First();
            }
        }

        public void GetCleanessStaticses(CleannessStatisticsViewModel model)
        {
            var hotels = Repo<HotelRestaurantRepository>().GetAllModels()
                .AddEqual(obj => obj.DistrictId == model.AreaGuid, model.AreaGuid)
                .AddEqual(item => item.StreetId == model.StreetGuid, model.StreetGuid)
                .AddEqual(hotel => hotel.AddressId == model.AddressGuid, model.AddressGuid);

            var endDate = new DateTime(model.DueDateTime.Year, model.DueDateTime.Month,
                DateTime.DaysInMonth(model.DueDateTime.Year, model.DueDateTime.Month));

            var areas = ProcessInvoke.GetInstance<UserDictionaryProcess>()
                .GetDictionaries(UserDictionaryType.Area, 0);

            var startDate = ReportStartDate(endDate, model.ReportType);
            var repo = Repo<MonitorDataRepository>().GetAllModels().Where(obj => obj.UpdateTime > startDate && obj.UpdateTime < endDate);
            var devices = Repo<RestaurantDeviceRepository>().GetAllModels();

            foreach (var area in areas)
            {
                var areaHotels = hotels.Where(obj => obj.DistrictId == area.Id).Select(item => item.Id).ToList();
                var faild = (from data in repo
                             where areaHotels.Contains(data.ProjectId.Value)
                                   && data.CommandData.DataName == ProtocolDataName.CleanerCurrent
                                   && data.DoubleValue < (from device in devices
                                                          where device.ProjectId == data.ProjectId
                                                          select device.LampblackDeviceModel).FirstOrDefault().Fail
                             select data).Count();

                var worse = (from data in repo
                             where areaHotels.Contains(data.ProjectId.Value)
                                   && data.CommandData.DataName == ProtocolDataName.CleanerCurrent
                                   && data.DoubleValue > (from device in devices
                                                          where device.ProjectId == data.ProjectId
                                                          select device.LampblackDeviceModel).FirstOrDefault().Fail
                                   && data.DoubleValue < (from device in devices
                                                          where device.ProjectId == data.ProjectId
                                                          select device.LampblackDeviceModel).FirstOrDefault().Worse
                             select data).Count();

                var qualified = (from data in repo
                                 where areaHotels.Contains(data.ProjectId.Value)
                                       && data.CommandData.DataName == ProtocolDataName.CleanerCurrent
                                       && data.DoubleValue > (from device in devices
                                                              where device.ProjectId == data.ProjectId
                                                              select device.LampblackDeviceModel).FirstOrDefault().Worse
                                       && data.DoubleValue < (from device in devices
                                                              where device.ProjectId == data.ProjectId
                                                              select device.LampblackDeviceModel).FirstOrDefault().Qualified
                                 select data).Count();

                var good = (from data in repo
                            where areaHotels.Contains(data.ProjectId.Value)
                                  && data.CommandData.DataName == ProtocolDataName.CleanerCurrent
                                  && data.DoubleValue > (from device in devices
                                                         where device.ProjectId == data.ProjectId
                                                         select device.LampblackDeviceModel).FirstOrDefault().Qualified
                            select data).Count();

                var fan = (from data in repo
                           where areaHotels.Contains(data.ProjectId.Value)
                                 && data.CommandData.DataName == ProtocolDataName.FanSwitch
                                 && data.BooleanValue == true
                           select data).Count();

                var record = new CleanessStatics
                {
                    FaildRunningTimeTicks = faild * 2 * TimeSpan.TicksPerMinute,
                    WorseRunningTimeTicks = worse * 2 * TimeSpan.TicksPerMinute,
                    QualifiedRunningTimeTicks = qualified * 2 * TimeSpan.TicksPerMinute,
                    GoodRunningTimeTicks = good * 2 * TimeSpan.TicksPerMinute,
                    FanRunningTimeTicks = fan * 2 * TimeSpan.TicksPerMinute,
                    AreaGuid = area.Id,
                    AreaName = area.ItemValue
                };

                model.CleanessStatics.Add(record);
            }
        }

        private DateTime ReportStartDate(DateTime dueDateTime, ReportType reportType)
        {
            switch (reportType)
            {
                case ReportType.Day:
                    return dueDateTime.AddDays(-1);
                case ReportType.Week:
                    return dueDateTime.AddDays(-7);
                case ReportType.Month:
                    return dueDateTime.AddMonths(-1);
                case ReportType.Season:
                    return dueDateTime.AddMonths(-3);
                case ReportType.Halfyear:
                    return dueDateTime.AddMonths(-6);
                case ReportType.Year:
                    return dueDateTime.AddYears(-1);
            }

            return DateTime.Now;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Platform.Process.IProcess;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.Model;
using SHWDTech.Platform.Model.Table;
using SHWDTech.Platform.Utility.ExtensionMethod;
using WebViewModels.Enums;
using WebViewModels.ViewModel;

namespace Platform.Process.Process
{
    /// <summary>
    /// 运行时间处理程序
    /// </summary>
    public class RunningTimeProcess : ProcessBase, IRunningTimeProcess
    {
        public DateTime LastRecordDateTime(long hotelIdentity, long deviceIdentity, RunningTimeType type)
        {
            using (var repo = Repo<RunningTimeRepository>())
            {
                var lastRecord = repo.GetModels(obj => obj.Type == type && obj.ProjectIdentity == hotelIdentity && obj.DeviceIdentity == deviceIdentity)
                    .OrderByDescending(item => item.UpdateTime).FirstOrDefault();

                return lastRecord?.UpdateTime ?? DateTime.MinValue;
            }
        }

        public void StoreRunningTime(RunningTime runningTime)
            => Repo<RunningTimeRepository>().AddOrUpdateDoCommit(runningTime);

        public Dictionary<string, double> GetRunningTimeReport(TrendAnalisysViewModel model)
        {
            var hotels = Repo<HotelRestaurantRepository>().GetAllModels()
                .AddEqual(obj => obj.DistrictId == model.AreaGuid, model.AreaGuid)
                .AddEqual(item => item.StreetId == model.StreetGuid, model.StreetGuid)
                .AddEqual(hotel => hotel.AddressId == model.AddressGuid, model.AddressGuid)
                .AddEqual(h => h.ProjectName == model.QueryName, model.QueryName)
                .Select(t => t.Identity).ToList();
            model.StartDateTime = model.StartDateTime.GetCurrentMonth();
            model.DueDateTime = model.DueDateTime.GetCurrentMonth();

            var dateList = GetProcessDateTimes(model);

            var linageList = new Dictionary<string, double>();
            using (var repo = Repo<RunningTimeRepository>())
            {
                var runTimes = repo.GetModels(obj => hotels.Contains(obj.ProjectIdentity));
                foreach (var date in dateList)
                {
                    linageList.Add(date.ToString("yyyy-MM"), GetLinkage(runTimes, date, model.ReportType));
                }
            }

            return linageList;
        }

        private List<DateTime> GetProcessDateTimes(TrendAnalisysViewModel model)
        {
            switch (model.ReportType)
            {
                case ReportType.Month:
                    return GetMonthDateRange(model);
                case ReportType.Season:
                    return GetSeasonDateRange(model);
                case ReportType.Halfyear:
                    return GetHalfYearDateRange(model);
                case ReportType.Year:
                    return GetYearDateRange(model);
            }

            return null;
        }

        private List<DateTime> GetMonthDateRange(TrendAnalisysViewModel model)
        {
            var months = model.DueDateTime.MonthDifference(model.StartDateTime);

            var monthDates = new List<DateTime>();
            var current = 0;
            while (current <= months)
            {
                monthDates.Add(model.StartDateTime);
                model.StartDateTime = model.StartDateTime.AddMonths(1);
                current++;
            }

            return monthDates;
        }

        private List<DateTime> GetSeasonDateRange(TrendAnalisysViewModel model)
        {
            var startDate = new DateTime(model.StartDateTime.Year, GetSeamonMonth(model.StartSeason), DateTime.DaysInMonth(model.StartDateTime.Year, GetSeamonMonth(model.StartSeason)));
            var endDate = new DateTime(model.DueDateTime.Year, GetSeamonMonth(model.EndSeason), DateTime.DaysInMonth(model.DueDateTime.Year, GetSeamonMonth(model.EndSeason)));
            var rounds = endDate.MonthDifference(startDate) / 3;

            var monthDates = new List<DateTime>();
            var current = 0;
            while (current <= rounds)
            {
                monthDates.Add(startDate);
                startDate = startDate.AddMonths(3);
                current++;
            }

            return monthDates;
        }

        private List<DateTime> GetHalfYearDateRange(TrendAnalisysViewModel model)
        {
            var startDate = new DateTime(model.StartDateTime.Year, ((int)model.StartHalfYear + 1) * 6, DateTime.DaysInMonth(model.StartDateTime.Year, ((int)model.StartHalfYear + 1) * 6));
            var endDate = new DateTime(model.DueDateTime.Year, ((int)model.EndHalfYear + 1) * 6, DateTime.DaysInMonth(model.DueDateTime.Year, ((int)model.EndHalfYear + 1) * 6));
            var rounds = endDate.MonthDifference(startDate) / 6;

            var monthDates = new List<DateTime>();
            var current = 0;
            while (current <= rounds)
            {
                monthDates.Add(startDate);
                startDate = startDate.AddMonths(6);
                current++;
            }

            return monthDates;
        }

        private List<DateTime> GetYearDateRange(TrendAnalisysViewModel model)
        {
            var startDate = new DateTime(model.StartDateTime.Year, 12, 31);
            var endDate = new DateTime(model.DueDateTime.Year, 12, 31);
            var rounds = endDate.MonthDifference(startDate) / 12;

            var monthDates = new List<DateTime>();
            var current = 0;
            while (current <= rounds)
            {
                monthDates.Add(startDate);
                startDate = startDate.AddMonths(12);
                current++;
            }

            return monthDates;
        }

        private int GetSeamonMonth(ReportSeason season)
        {
            switch (season)
            {
                case ReportSeason.FirstSeason:
                    return 3;
                case ReportSeason.SecondSeason:
                    return 6;
                case ReportSeason.ThridSeason:
                    return 9;
                case ReportSeason.FourthSeason:
                    return 12;
            }

            return 0;
        }

        private double GetLinkage(IQueryable<RunningTime> queryable, DateTime dueDateTime, ReportType reportType)
        {
            var fan = GetRunTimeTicks(queryable, dueDateTime, reportType, RunningTimeType.Fan);

            if (fan == 0) return 0.0;

            var cleaner = GetRunTimeTicks(queryable, dueDateTime, reportType, RunningTimeType.Cleaner);

            return Math.Round(fan * 1.0 / cleaner, 2);
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

        private long GetRunTimeTicks(IQueryable<RunningTime> queryable, DateTime dueDateTime, ReportType reportType, RunningTimeType runTimeType)
        {
            var startDate = ReportStartDate(dueDateTime, reportType);

            var runningTime = queryable.Where(obj =>
                    obj.UpdateTime >= startDate
                    && obj.UpdateTime < dueDateTime
                    && obj.Type == runTimeType);
            return runningTime.Count() != 0 ? runningTime.Sum(item => item.RunningTimeTicks) : 0;
        }

        public List<RunningTimeTable> GetRunningTimeTables(List<RestaurantDevice> devs, DateTime startDateTime, DateTime endDateTime)
        {
            var rows = devs.Select(d => new RunningTimeTable
            {
                DistrictName = GetDistrictName(d.Hotel.DistrictId),
                ProjectName = d.Hotel.ProjectName,
                DeviceName = d.DeviceName,
                DeviceRunningTime = GetRunningTimes(startDateTime, endDateTime, RunningTimeType.Device, d.Hotel.Identity, d.Identity),
                CleanerRunningTime = GetRunningTimes(startDateTime, endDateTime, RunningTimeType.Cleaner, d.Hotel.Identity, d.Identity),
                FanRunningTime = GetRunningTimes(startDateTime, endDateTime, RunningTimeType.Fan, d.Hotel.Identity, d.Identity)
            }).ToList();
            return rows;
        }

        public List<LinkageRateTable> GetLinkageRateTables(List<RestaurantDevice> devs, DateTime queryDateTime)
        {
            return null;
        }

        private string GetRunningTimes(DateTime startDateTime, DateTime endDateTime, RunningTimeType type,
            long hotelIdentity, long deviceIdentity)
        {
            long ticks = 0;
            using (var repo = Repo<RunningTimeRepository>())
            {
                var query = repo.GetModels(r => r.Type == type && r.ProjectIdentity == hotelIdentity &&
                                    r.DeviceIdentity == deviceIdentity
                                    && r.UpdateTime > startDateTime && r.UpdateTime < endDateTime);
                if (query.Any())
                {
                    ticks = query.Sum(q => q.RunningTimeTicks);
                }
            }

            var timeSpan = TimeSpan.FromTicks(ticks);
            return $"{timeSpan.Days * 24 + timeSpan.Hours}小时{timeSpan.Minutes}分钟";
        }
    }
}

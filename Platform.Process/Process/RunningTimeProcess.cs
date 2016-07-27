using System;
using System.Collections.Generic;
using System.Linq;
using Platform.Process.IProcess;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.Model;
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
        public DateTime LastRecordDateTime(Guid hotelGuid, RunningTimeType type)
        {
            using (var repo = Repo<RunningTimeRepository>())
            {
                var lastRecord = repo.GetModels(obj => obj.ProjectId == hotelGuid && obj.Type == type)
                    .OrderByDescending(item => item.UpdateTime).FirstOrDefault();

                return lastRecord?.UpdateTime ?? DateTime.MinValue;
            }
        }

        public void StoreRunningTime(RunningTime runningTime)
            => Repo<RunningTimeRepository>().AddOrUpdateDoCommit(runningTime);

        public List<double> GetRunningTimeReport(TrendAnalisysViewModel model)
        {
            var hotels = Repo<HotelRestaurantRepository>().GetAllModels()
                .AddEqual(obj => obj.DistrictId == model.AreaGuid, model.AreaGuid)
                .AddEqual(item => item.StreetId == model.StreetGuid, model.StreetGuid)
                .AddEqual(hotel => hotel.AddressId == model.AddressGuid, model.AddressGuid)
                .AddEqual(h => h.ProjectName == model.QueryName, model.QueryName)
                .Select(t => t.Id).ToList();
            model.StartDateTime = model.StartDateTime.GetCurrentMonth();
            model.DueDateTime = model.DueDateTime.GetCurrentMonth();

            var dateList = GetProcessDateTimes(model);

            var linageList = new List<double>();
            using (var repo = Repo<RunningTimeRepository>())
            {
                var runTimes = repo.GetModels(obj => hotels.Contains(obj.ProjectId));
                foreach (var date in dateList)
                {
                    linageList.Add(GetLinkage(runTimes, date, model.ReportType));
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

            }

            return null;
        }

        private List<DateTime> GetMonthDateRange(TrendAnalisysViewModel model)
        {
            var months = model.DueDateTime.MonthDifference(model.StartDateTime);

            var monthDates = new List<DateTime>();
            for (var i = 0; i < months; i++)
            {
                var date = model.StartDateTime.AddMonths(i + 1);

                monthDates.Add(date);
            }

            return monthDates;
        }

        private List<DateTime> GetSeasonDateRange(TrendAnalisysViewModel model)
        {
            var rounds = (new DateTime(model.StartDateTime.Year, (int)model.StartSeason * 3 + 1, 0)
                .MonthDifference(new DateTime(model.DueDateTime.Year, (int)model.EndSeason * 3 + 1, 0)))
                / 3;

            var monthDates = new List<DateTime>();
            for (var i = 0; i < rounds; i++)
            {
                var date = model.StartDateTime.AddMonths(i + 3);

                monthDates.Add(date);
            }

            return monthDates;
        }

        private double GetLinkage(IQueryable<RunningTime> queryable, DateTime dueDateTime, ReportType reportType)
        {
            var fan = GetRunTimeTicks(queryable, dueDateTime, reportType, RunningTimeType.Fan);

            if (fan == 0) return 0.0;

            var cleaner = GetRunTimeTicks(queryable, dueDateTime, reportType, RunningTimeType.Cleaner);

            return fan * 1.0 / cleaner;
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
            if (runningTime.Count() != 0)
            {
                return runningTime.Sum(item => item.RunningTimeTicks);
            }

            return 0;
        }
    }
}

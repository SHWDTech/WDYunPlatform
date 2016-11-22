using System;
using System.Linq;
using Platform.Process.Enums;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.Model;
using SHWDTech.Platform.Utility.ExtensionMethod;
using WebViewModels.Enums;
using WebViewModels.ViewDataModel;
using WebViewModels.ViewModel;

namespace Platform.Process.Process
{
    public class ReportProcess : ProcessBase
    {
        public void GetGeneralReportViewModel(GeneralReportViewModel model)
        {
            var hotels = Repo<HotelRestaurantRepository>().GetAllModels()
                .AddEqual(obj => obj.DistrictId == model.AreaGuid, model.AreaGuid)
                .AddEqual(item => item.StreetId == model.StreetGuid, model.StreetGuid)
                .AddEqual(hotel => hotel.AddressId == model.AddressGuid, model.AddressGuid);

            var startDate = ReportStartDate(model.DueDateTime, model.ReportType).GetToday();

            using (var repo = Repo<RunningTimeRepository>())
            {
                foreach (var hotel in hotels)
                {
                    var report = new GeneralReport
                    {
                        HotelGuid = hotel.Id,
                        HotelName = hotel.ProjectName
                    };

                    if (repo.GetCount(obj => obj.ProjectId == hotel.Id) <= 0)
                    {
                        report.TotalCleanerRunTimeTicks = 0;
                        report.TotalFanRunTimeTicks = 0;
                        model.GeneralReports.Add(report);
                        continue;
                    }

                    var cleanerRecord = repo.GetModels(obj =>
                        obj.UpdateTime >= startDate
                        && obj.UpdateTime < model.DueDateTime
                        && obj.ProjectId == hotel.Id
                        && obj.Type == RunningTimeType.Cleaner);
                    if (cleanerRecord.Count() != 0)
                    {
                        report.TotalCleanerRunTimeTicks = cleanerRecord.Sum(item => item.RunningTimeTicks);
                    }

                    var fanRecord = repo.GetModels(obj =>
                        obj.UpdateTime >= startDate
                        && obj.UpdateTime < model.DueDateTime
                        && obj.ProjectId == hotel.Id
                        && obj.Type == RunningTimeType.Fan);

                    if (fanRecord.Count() != 0)
                    {
                        report.TotalFanRunTimeTicks = fanRecord.Sum(item => item.RunningTimeTicks);
                    }

                    model.GeneralReports.Add(report);
                }
            }

        }

        public void GetGeneralComparisonViewModel(GeneralComparisonViewModel model)
        {
            var hotels = Repo<HotelRestaurantRepository>().GetAllModels()
                .AddEqual(obj => obj.DistrictId == model.AreaGuid, model.AreaGuid)
                .AddEqual(item => item.StreetId == model.StreetGuid, model.StreetGuid)
                .AddEqual(hotel => hotel.AddressId == model.AddressGuid, model.AddressGuid);

            var areas = Invoke<UserDictionaryProcess>()
                .GetDictionaries(UserDictionaryType.Area, 0);

            var repo = Repo<RunningTimeRepository>();
            foreach (var userDictionary in areas)
            {
                var record = new GeneralCompasion()
                {
                    AreaGuid = userDictionary.Id,
                    AreaName = userDictionary.ItemValue
                };
                var areaHotels = hotels.Where(obj => obj.DistrictId == userDictionary.Id).Select(item => item.Id).ToList();
                var hotelRunningTimes = repo.GetModels(obj => areaHotels.Contains(obj.ProjectId));
                record.CurrentLinkage = GetLinkage(hotelRunningTimes, model.DueDateTime, model.ReportType);

                var lastdueDateTime = ReportStartDate(model.DueDateTime, model.ReportType);

                record.LinkedLinkage = GetLinkage(hotelRunningTimes, lastdueDateTime, model.ReportType);

                record.LastLinkage = GetLinkage(hotelRunningTimes, model.DueDateTime.AddYears(-1), model.ReportType);

                model.GeneralReports.Add(record);
            }
        }

        private double GetLinkage(IQueryable<RunningTime> queryable, DateTime dueDateTime, ReportType reportType)
        {
            var fan = GetRunTimeTicks(queryable, dueDateTime, reportType, RunningTimeType.Fan);

            if (fan == 0) return 0.0;

            var cleaner = GetRunTimeTicks(queryable, dueDateTime, reportType, RunningTimeType.Cleaner);

            return fan * 1.0/cleaner;
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

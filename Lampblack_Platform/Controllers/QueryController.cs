using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Lampblack_Platform.Models.BootstrapTable;
using Lampblack_Platform.Models.Query;
using MvcWebComponents.Attributes;
using MvcWebComponents.Controllers;
using MvcWebComponents.Filters;
using Platform.Process.Process;
using SHWDTech.Platform.Model.Model;

namespace Lampblack_Platform.Controllers
{
    [AjaxGet]
    public class QueryController : WdControllerBase
    {
        // GET: Query
        public ActionResult CleanRate() => View();

        [NamedAuth(Modules = "CleanRate", Required = true)]
        public ActionResult CleanRateTable(CleanRateDataTable post)
        {
            var query = ProcessInvoke<RestaurantDeviceProcess>().GetRestaurantDeviceByArea(post.Area, post.Street, post.Address);
            if (!string.IsNullOrWhiteSpace(post.Name))
            {
                query = query.Where(d => d.Project.ProjectName.Contains(post.Name));
            }
            var total = query.Count();
            var devs = query.Include("Hotel").Include("LampblackDeviceModel").OrderBy(d => new
                {
                    d.ProjectId,
                    d.Identity
                }).Skip(post.offset)
                .Take(post.limit).ToList();
            var merge = devs.GroupBy(d => d.Hotel.Id).Where(e => e.Count() > 1)
                .Select(v => new
                {
                    index = devs.IndexOf(devs.First(d => d.Hotel.Id == v.Key)),
                    count = v.Count()
                }).ToList();

            var rows = ProcessInvoke<HotelRestaurantProcess>().GetCleanRateTables(devs, post.StartDate, post.EndDate);


            return JsonTable(new
            {
                total,
                rows,
                merge
            });
        }

        public ActionResult LinkageRate() => View();

        [NamedAuth(Modules = "LinkageRate", Required = true)]
        public ActionResult LinkageRateTable(LinkageRateTable post)
        {
            var query = ProcessInvoke<RestaurantDeviceProcess>().GetRestaurantDeviceByArea(post.Area, post.Street, post.Address);
            if (!string.IsNullOrWhiteSpace(post.Name))
            {
                query = query.Where(d => d.Project.ProjectName.Contains(post.Name));
            }
            var total = query.Count();
            var devs = query.Include("Hotel").OrderBy(d => new
                {
                    d.ProjectId,
                    d.Identity
                }).Skip(post.offset)
                .Take(post.limit).ToList();
            var merge = devs.GroupBy(d => d.Hotel.Id).Where(e => e.Count() > 1)
                .Select(v => new
                {
                    index = devs.IndexOf(devs.First(d => d.Hotel.Id == v.Key)),
                    count = v.Count()
                }).ToList();

            var rows = ProcessInvoke<RunningTimeProcess>().GetLinkageRateTables(devs, post.QueryDateTime);

            return JsonTable(new
            {
                total,
                rows,
                merge
            });
        }

        public ActionResult RemovalRate() => View();

        public ActionResult Alarm(AlarmViewModel model)
        {
            var page = string.IsNullOrWhiteSpace(Request["page"]) ? 1 : int.Parse(Request["page"]);

            var pageSize = string.IsNullOrWhiteSpace(Request["pageSize"]) ? 10 : int.Parse(Request["pageSize"]);

            var queryName = Request["queryName"];

            int count;

            var conditions = new List<Expression<Func<Alarm, bool>>>();

            if (model.StartDateTime == DateTime.MinValue)
            {
                model.StartDateTime = DateTime.Now.AddDays(-7);
            }

            Expression<Func<Alarm, bool>> startCondition = ex => ex.UpdateTime > model.StartDateTime;
            conditions.Add(startCondition);

            if (model.EndDateTime == DateTime.MinValue)
            {
                model.EndDateTime = DateTime.Now;
            }

            Expression<Func<Alarm, bool>> endCondition = ex => ex.UpdateTime < model.EndDateTime;
            conditions.Add(endCondition);

            var alarmView = ProcessInvoke<HotelRestaurantProcess>()
                .GetPagedAlarm(page, pageSize, queryName, out count, conditions);

            model.PageIndex = page;
            model.PageSize = pageSize;
            model.QueryName = queryName;
            model.Count = count;
            model.AlarmView = alarmView;
            model.PageCount = (count % pageSize) > 0 ? (count / pageSize) + 1 : (count / pageSize);

            return View(model);
        }


        public ActionResult HistoryData() => View();

        [NamedAuth(Modules = "HistoryData", Required = true)]
        public ActionResult HistoryDataTable(HistoryDataTable post)
        {
            if (post.Hotel == Guid.Empty) return null;
            var hotel = ProcessInvoke<HotelRestaurantProcess>().GetHotelById(post.Hotel);
            var query = ProcessInvoke<LampblackRecordProcess>().GetRecordRepo();
            query = query.Where(obj => obj.ProjectIdentity == hotel.Identity && obj.RecordDateTime > post.StartDate && obj.RecordDateTime < post.EndDate);
            var total = query.Count();
            if (total == 0) return null;
            var records = query.OrderByDescending(o => o.RecordDateTime).Skip(post.offset).Take(post.limit).ToList();
            var devIdentity = records[0].DeviceIdentity;
            var dev = ProcessInvoke<RestaurantDeviceProcess>().AllDevices().First(d => d.Identity == devIdentity);
            var districtName = ProcessBase.GetDistrictName(hotel.DistrictId);
            var rows = (from record in records
                        select new HistoryDataTableRows
                        {
                            DistrictName = districtName,
                            HotelName = $"{dev.Hotel.RaletedCompany.CompanyName}({dev.Hotel.ProjectName})",
                            DeviceName = dev.DeviceName,
                            CleanerSwitch = record.CleanerSwitch,
                            CleanerCurrent = record.CleanerCurrent,
                            FanSwitch = record.FanSwitch,
                            FanCurrent = record.FanCurrent,
                            DateTime = $"{record.RecordDateTime:yyyy-MM-dd HH:mm:ss}"
                        }).ToList();

            return JsonTable(new
            {
                total,
                rows
            });
        }

        public ActionResult RunningTime() => View();

        [NamedAuth(Modules = "RunningTime", Required = true)]
        public ActionResult RunningTimeTable(RunngingDataTable post)
        {
            var query = ProcessInvoke<RestaurantDeviceProcess>().GetRestaurantDeviceByArea(post.Area, post.Street, post.Address);
            if (!string.IsNullOrWhiteSpace(post.Name))
            {
                query = query.Where(d => d.Project.ProjectName.Contains(post.Name));
            }
            var total = query.Count();
            var devs = query.Include("Hotel").OrderBy(d => new
                {
                    d.ProjectId,
                    d.Identity
                }).Skip(post.offset)
                .Take(post.limit).ToList();
            var merge = devs.GroupBy(d => d.Hotel.Id).Where(e => e.Count() > 1)
                .Select(v => new
                {
                    index = devs.IndexOf(devs.First(d => d.Hotel.Id == v.Key)),
                    count = v.Count()
                }).ToList();

            var rows = ProcessInvoke<RunningTimeProcess>().GetRunningTimeTables(devs, post.StartDate, post.EndDate);

            return JsonTable(new
            {
                total,
                rows,
                merge
            });
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using Lampblack_Platform.Models.Query;
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
        public ActionResult CleanRate(CleanRateViewModel model)
        {
            var page = string.IsNullOrWhiteSpace(Request["page"]) ? 1 : int.Parse(Request["page"]);

            var pageSize = string.IsNullOrWhiteSpace(Request["pageSize"]) ? 10 : int.Parse(Request["pageSize"]);

            var queryName = Request["queryName"];

            int count;

            var conditions = new List<Expression<Func<DataStatistics, bool>>>();

            if (model.StartDateTime == DateTime.MinValue)
            {
                model.StartDateTime = DateTime.Now.AddDays(-7);
            }

            Expression<Func<DataStatistics, bool>> startCondition = ex => ex.UpdateTime > model.StartDateTime;
            conditions.Add(startCondition);

            if (model.EndDateTime == DateTime.MinValue)
            {
                model.EndDateTime = DateTime.Now;
            }

            Expression<Func<DataStatistics, bool>> endCondition = ex => ex.UpdateTime < model.EndDateTime;
            conditions.Add(endCondition);

            var cleanRateView = ProcessInvoke<HotelRestaurantProcess>()
                .GetPagedCleanRateView(page, pageSize, queryName, out count, conditions);

            model.PageIndex = page;
            model.PageSize = pageSize;
            model.QueryName = queryName;
            model.Count = count;
            model.CleanRateView = cleanRateView;
            model.PageCount = (count%pageSize) > 0 ? (count/pageSize) + 1 : (count/pageSize);
            
            return View(model);
        }

        public ActionResult LinkageRate(LinkageViewModel model)
        {
            var page = string.IsNullOrWhiteSpace(Request["page"]) ? 1 : int.Parse(Request["page"]);

            var pageSize = string.IsNullOrWhiteSpace(Request["pageSize"]) ? 10 : int.Parse(Request["pageSize"]);

            var queryName = Request["queryName"];

            int count;

            var conditions = new List<Expression<Func<RunningTime, bool>>>();

            if (model.StartDateTime == DateTime.MinValue)
            {
                model.StartDateTime = DateTime.Now.AddDays(-7);
            }

            Expression<Func<RunningTime, bool>> startCondition = ex => ex.UpdateTime > model.StartDateTime;
            conditions.Add(startCondition);

            if (model.EndDateTime == DateTime.MinValue)
            {
                model.EndDateTime = DateTime.Now;
            }

            Expression<Func<RunningTime, bool>> endCondition = ex => ex.UpdateTime < model.EndDateTime;
            conditions.Add(endCondition);

            var linkageView = ProcessInvoke<HotelRestaurantProcess>()
                .GetPagedLinkage(page, pageSize, queryName, out count, conditions);

            model.PageIndex = page;
            model.PageSize = pageSize;
            model.QueryName = queryName;
            model.Count = count;
            model.LinkageView = linkageView;
            model.PageCount = (count % pageSize) > 0 ? (count / pageSize) + 1 : (count / pageSize);

            return View(model);
        }

        public ActionResult RemovalRate()
        {
            return View();
        }

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

        public ActionResult HistoryData(HistoryDataViewModel model)
        {
            var page = string.IsNullOrWhiteSpace(Request["page"]) ? 1 : int.Parse(Request["page"]);

            var pageSize = string.IsNullOrWhiteSpace(Request["pageSize"]) ? 10 : int.Parse(Request["pageSize"]);

            var queryName = Request["queryName"];

            int count;

            var conditions = new List<Expression<Func<MonitorData, bool>>>();

            if (model.StartDateTime == DateTime.MinValue)
            {
                model.StartDateTime = DateTime.Now.AddDays(-7);
            }

            Expression<Func<MonitorData, bool>> startCondition = ex => ex.UpdateTime > model.StartDateTime;
            conditions.Add(startCondition);

            if (model.EndDateTime == DateTime.MinValue)
            {
                model.EndDateTime = DateTime.Now;
            }

            Expression<Func<MonitorData, bool>> endCondition = ex => ex.UpdateTime < model.EndDateTime;
            conditions.Add(endCondition);

            var historyData = ProcessInvoke<HotelRestaurantProcess>()
                .GetPagedHistoryData(page, pageSize, queryName, out count, conditions);

            model.PageIndex = page;
            model.PageSize = pageSize;
            model.QueryName = queryName;
            model.Count = count;
            model.HistoryData = historyData;
            model.PageCount = (count % pageSize) > 0 ? (count / pageSize) + 1 : (count / pageSize);

            return View(model);
        }

        public ActionResult RunningTime(RunningTimeViewModel model)
        {
            var page = string.IsNullOrWhiteSpace(Request["page"]) ? 1 : int.Parse(Request["page"]);

            var pageSize = string.IsNullOrWhiteSpace(Request["pageSize"]) ? 10 : int.Parse(Request["pageSize"]);

            var queryName = Request["queryName"];

            int count;

            var conditions = new List<Expression<Func<RunningTime, bool>>>();

            if (model.StartDateTime == DateTime.MinValue)
            {
                model.StartDateTime = DateTime.Now.AddDays(-7);
            }

            Expression<Func<RunningTime, bool>> startCondition = ex => ex.UpdateTime > model.StartDateTime;
            conditions.Add(startCondition);

            if (model.EndDateTime == DateTime.MinValue)
            {
                model.EndDateTime = DateTime.Now;
            }

            Expression<Func<RunningTime, bool>> endCondition = ex => ex.UpdateTime < model.EndDateTime;
            conditions.Add(endCondition);

            var runningTimeView = ProcessInvoke<HotelRestaurantProcess>()
                .GetPagedRunningTime(page, pageSize, queryName, out count, conditions);

            model.PageIndex = page;
            model.PageSize = pageSize;
            model.QueryName = queryName;
            model.Count = count;
            model.RunningTimeView = runningTimeView;
            model.PageCount = (count % pageSize) > 0 ? (count / pageSize) + 1 : (count / pageSize);

            return View(model);
        }
    }
}
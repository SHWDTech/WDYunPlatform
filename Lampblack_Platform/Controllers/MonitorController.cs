﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Lampblack_Platform.Models.Monitor;
using MvcWebComponents.Attributes;
using MvcWebComponents.Controllers;
using MvcWebComponents.Filters;
using Platform.Process;
using Platform.Process.Process;

namespace Lampblack_Platform.Controllers
{
    [AjaxGet]
    public class MonitorController : WdControllerBase
    {
        [AjaxGet]
        public ActionResult Map() => DefaultView();

        public ActionResult MapHotel()
        {
            var page = string.IsNullOrWhiteSpace(Request["page"]) ? 1 : int.Parse(Request["page"]);

            var pageSize = string.IsNullOrWhiteSpace(Request["pageSize"]) ? 10 : int.Parse(Request["pageSize"]);

            var queryName = Request["queryName"];

            int count;

            var hotelList = ProcessInvoke.GetInstance<HotelRestaurantProcess>()
                .GetPagedHotelRestaurant(page, pageSize, queryName, out count)
                .ToDictionary(obj => obj.Id, item => item.ProjectName);

            var model = new MapHotelViewModel
            {
                Count = count,
                PageSize = pageSize,
                QueryName = queryName,
                PageCount = (count % pageSize) > 0 ? (count / pageSize) + 1 : (count / pageSize),
                PageIndex = page,
                Hotels = hotelList
            };

            GetMapHotelRelatedItems();

            return View(model);
        }

        [HttpPost]
        [NamedAuth(Modules = "Map")]
        public ActionResult MapHotel(MapHotelViewModel model)
        {
           

            return View(model);
        }

        private void GetMapHotelRelatedItems()
        {
            ViewBag.AreaListItems = new List<SelectListItem>
            {
                new SelectListItem() {Text = "全部", Value = "" }
            };

            ViewBag.AreaListItems.AddRange(ProcessInvoke.GetInstance<UserDictionaryProcess>()
                .GetDistrictSelectList()
                .Select(obj => new SelectListItem() { Text = obj.Key, Value = obj.Value })
                .ToList());
        }
    }
}
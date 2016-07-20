using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Lampblack_Platform.Models.Monitor;
using MvcWebComponents.Attributes;
using MvcWebComponents.Controllers;
using MvcWebComponents.Filters;
using MvcWebComponents.Model;
using Platform.Process;
using Platform.Process.Process;
using SHWDTech.Platform.Model.Model;

namespace Lampblack_Platform.Controllers
{
    [AjaxGet]
    public class MonitorController : WdControllerBase
    {
        public ActionResult Map() => DefaultView();

        [NamedAuth(Modules = "Map")]
        public ActionResult GetHotelInfo()
        {
            var hotelLocation = ProcessInvoke.GetInstance<HotelRestaurantProcess>().GetHotelLocations();
            return Json(new JsonStruct()
            {
                Success = true,
                Result = hotelLocation
            },JsonRequestBehavior.AllowGet);
        }

        [NamedAuth(Modules = "Map")]
        public ActionResult GetMapHotelInfo(Guid hotelGuid)
        {
            var hotelLocation = ProcessInvoke.GetInstance<HotelRestaurantProcess>().GetMapHotelCurrentStatus(hotelGuid);
            return Json(new JsonStruct()
            {
                Success = true,
                Result = hotelLocation
            }, JsonRequestBehavior.AllowGet);
        }

        [NamedAuth(Modules = "Map")]
        public ActionResult MapHotel()
        {
            var page = string.IsNullOrWhiteSpace(Request["page"]) ? 1 : int.Parse(Request["page"]);

            var pageSize = string.IsNullOrWhiteSpace(Request["pageSize"]) ? 15 : int.Parse(Request["pageSize"]);

            var queryName = Request["queryName"];

            int count;

            var area = string.IsNullOrWhiteSpace(Request["AreaGuid"]) ? Guid.Empty : Guid.Parse(Request["AreaGuid"]);

            var street = string.IsNullOrWhiteSpace(Request["StreetGuid"]) ? Guid.Empty : Guid.Parse(Request["StreetGuid"]);

            var conditions = new List<Expression<Func<HotelRestaurant, bool>>>();
            var paramsObjects = new Dictionary<string, string>();

            if (area != Guid.Empty)
            {
                Expression<Func<HotelRestaurant, bool>> condition = ex => ex.DistrictId == area;
                conditions.Add(condition);
                paramsObjects.Add("area", area.ToString());
            }

            if (street != Guid.Empty)
            {
                Expression<Func<HotelRestaurant, bool>> condition = ex => ex.StreetId == street;
                conditions.Add(condition);
                paramsObjects.Add("street", street.ToString());
            }

            var hotelList = ProcessInvoke.GetInstance<HotelRestaurantProcess>()
                .GetPagedHotelRestaurant(page, pageSize, queryName, out count, conditions);

            var model = new MapHotelViewModel
            {
                Count = count,
                PageSize = pageSize,
                QueryName = queryName,
                PageCount = (count % pageSize) > 0 ? (count / pageSize) + 1 : (count / pageSize),
                PageIndex = page,
                Hotels = hotelList
            };

            GetMapHotelRelatedItems(paramsObjects, model);

            return View(model);
        }

        public ActionResult Actual()
        {
            return View();
        }

        private void GetMapHotelRelatedItems(Dictionary<string, string> paramsObjects, MapHotelViewModel model)
        {
            var areaList = new List<SelectListItem>
                {
                    new SelectListItem() {Text = "全部", Value = "" }
                };

            var streetList = new List<SelectListItem>
                {
                    new SelectListItem() {Text = "全部", Value = ""}
                };

            areaList.AddRange(ProcessInvoke.GetInstance<UserDictionaryProcess>()
                .GetDistrictSelectList()
                .Select(obj => new SelectListItem() { Text = obj.Value, Value = obj.Key.ToString() })
                .ToList());

            if (paramsObjects.ContainsKey("area"))
            {
                var selectArea = paramsObjects["area"];

                model.AreaGuid = Guid.Parse(selectArea);

                streetList.AddRange(ProcessInvoke.GetInstance<UserDictionaryProcess>()
                    .GetChildDistrict(Guid.Parse(selectArea))
                    .Select(obj => new SelectListItem() { Text = obj.Value, Value = obj.Key.ToString() })
                    .ToList());

                if (paramsObjects.ContainsKey("street"))
                {
                    var selectStreet = paramsObjects["street"];
                    model.StreetGuid = Guid.Parse(selectStreet);
                }
            }

            ViewBag.AreaListItems = areaList;
            ViewBag.StreetListItems = streetList;
        }
    }
}
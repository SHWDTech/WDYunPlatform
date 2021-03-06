﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Lampblack_Platform.Models.Monitor;
using MvcWebComponents.Attributes;
using MvcWebComponents.Controllers;
using MvcWebComponents.Filters;
using MvcWebComponents.Model;
using Platform.Cache;
using Platform.Process.Process;
using SHWDTech.Platform.Model.Model;
using WebViewModels.ViewDataModel;

namespace Lampblack_Platform.Controllers
{
    [AjaxGet]
    public class MonitorController : WdControllerBase
    {
        public ActionResult Map() => DefaultView();

        [NamedAuth(Modules = "Map")]
        public ActionResult GetHotelInfo()
        {
            var hotelLocation =((List<HotelLocations>)PlatformCaches.GetCache("HotelLocations").CacheItem);
            if (WdContext.UserDistricts != null)
            {
                hotelLocation = hotelLocation.Where(obj => WdContext.UserDistricts.Contains(obj.DistrictGuid)).ToList();
            }
                return Json(new JsonStruct()
            {
                Result = hotelLocation
            }, JsonRequestBehavior.AllowGet);
        }

        [NamedAuth(Modules = "Map")]
        public ActionResult GetMapHotelInfo(Guid hotelGuid)
        {
            var hotelLocation = ProcessInvoke<HotelRestaurantProcess>().GetMapHotelCurrentStatus(hotelGuid);
            return Json(new JsonStruct()
            {
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

            var hotelList = ProcessInvoke<HotelRestaurantProcess>()
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
            var page = string.IsNullOrWhiteSpace(Request["page"]) ? 1 : int.Parse(Request["page"]);

            var pageSize = string.IsNullOrWhiteSpace(Request["pageSize"]) ? 15 : int.Parse(Request["pageSize"]);

            var queryName = Request["queryName"];

            int count;

            Guid area;
            Guid.TryParse(Request["AreaGuid"], out area);

            Guid street;
            Guid.TryParse(Request["StreetGuid"], out street);

            Guid address;
            Guid.TryParse(Request["AddressGuid"], out address);

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

            if (address != Guid.Empty)
            {
                Expression<Func<HotelRestaurant, bool>> condition = ex => ex.AddressId == address;
                conditions.Add(condition);
                paramsObjects.Add("address", street.ToString());
            }

            var hotelList = ProcessInvoke<HotelRestaurantProcess>()
                .GetPagedHotelStatus(page, pageSize, queryName, out count, conditions);

            var model = new ActualViewModel()
            {
                Count = count,
                PageSize = pageSize,
                QueryName = queryName,
                PageCount = (count % pageSize) > 0 ? (count / pageSize) + 1 : (count / pageSize),
                PageIndex = page,
                HotelsStatus = hotelList
            };

            GetActualRelatedItems(paramsObjects, model);

            return View(model);
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

            areaList.AddRange(ProcessInvoke<UserDictionaryProcess>()
                .GetDistrictSelectList()
                .Select(obj => new SelectListItem() { Text = obj.Value, Value = obj.Key.ToString() })
                .ToList());

            if (paramsObjects.ContainsKey("area"))
            {
                var selectArea = paramsObjects["area"];

                model.AreaGuid = Guid.Parse(selectArea);

                streetList.AddRange(ProcessInvoke<UserDictionaryProcess>()
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

        private void GetActualRelatedItems(Dictionary<string, string> paramsObjects, ActualViewModel model)
        {
            var areaList = new List<SelectListItem>
                {
                    new SelectListItem() {Text = "全部", Value = "" }
                };

            var streetList = new List<SelectListItem>
                {
                    new SelectListItem() {Text = "全部", Value = ""}
                };

            var addressList = new List<SelectListItem>
                {
                    new SelectListItem() {Text = "全部", Value = ""}
                };

            areaList.AddRange(ProcessInvoke<UserDictionaryProcess>()
                .GetDistrictSelectList()
                .Select(obj => new SelectListItem() { Text = obj.Value, Value = obj.Key.ToString() })
                .ToList());

            if (paramsObjects.ContainsKey("area"))
            {
                var selectArea = paramsObjects["area"];

                model.AreaGuid = Guid.Parse(selectArea);

                streetList.AddRange(ProcessInvoke<UserDictionaryProcess>()
                    .GetChildDistrict(Guid.Parse(selectArea))
                    .Select(obj => new SelectListItem() { Text = obj.Value, Value = obj.Key.ToString() })
                    .ToList());

                if (paramsObjects.ContainsKey("street"))
                {
                    var selectStreet = paramsObjects["street"];
                    model.StreetGuid = Guid.Parse(selectStreet);
                    addressList.AddRange(ProcessInvoke<UserDictionaryProcess>()
                    .GetChildDistrict(Guid.Parse(selectArea))
                    .Select(obj => new SelectListItem() { Text = obj.Value, Value = obj.Key.ToString() })
                    .ToList());

                }

                if (paramsObjects.ContainsKey("address"))
                {
                    var selectAddress = paramsObjects["address"];
                    model.AddressGuid = Guid.Parse(selectAddress);
                }
            }

            model.AreaListItems = areaList;
            model.StreetListItems = streetList;
            model.AddressListItems = addressList;
        }
    }
}
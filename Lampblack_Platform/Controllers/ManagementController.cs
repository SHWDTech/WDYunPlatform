using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Lampblack_Platform.Models.Management;
using MvcWebComponents.Controllers;
using MvcWebComponents.Filters;
using MvcWebComponents.Model;
using Platform.Process;
using Platform.Process.Process;
using SHWDTech.Platform.Model.Model;

namespace Lampblack_Platform.Controllers
{
    public class ManagementController : WdControllerBase
    {
        [AjaxGet]
        public ActionResult Area() 
            => View();

        [AjaxGet]
        public ActionResult GetAreaInfo()
        {
            var areaInfo = ProcessInvoke.GetInstance<UserDictionaryProcess>().GetAreaInfo();
            return Json(areaInfo, JsonRequestBehavior.AllowGet);
        }

        [AjaxGet]
        public ActionResult GetAreaList()
        {
            var areaId = Request["id"];
            if (string.IsNullOrWhiteSpace(areaId)) return null;
            var parent = Guid.Parse(areaId);

            var list = ProcessInvoke.GetInstance<UserDictionaryProcess>().GetChildDistrict(parent);

            return Json(new JsonStruct() {
                Success = true,
                Result = list.Select(obj => new {Id = obj.Key, ItemValue = obj.Value})}, 
                JsonRequestBehavior.AllowGet);
        }

        [AjaxGet]
        public ActionResult AddAreaInfo()
        {
            var areaName = Request["areaName"];

            var itemLevel = int.Parse(Request["itemLevel"]);

            Guid parentNode;

            Guid.TryParse(Request["parentNode"], out parentNode);

            var district = ProcessInvoke.GetInstance<UserDictionaryProcess>().AddArea(areaName, itemLevel, parentNode);

            return district == null
                ? Json(new JsonStruct() { Success = false, Message = "添加区县信息失败，请重新尝试。" }, JsonRequestBehavior.AllowGet)
                : Json("添加成功！", district, JsonRequestBehavior.AllowGet);
        }

        [AjaxGet]
        public ActionResult EditAreaInfo()
        {
            var id = Guid.Parse(Request["itemId"]);
            var editName = Request["editName"];
            var district = ProcessInvoke.GetInstance<UserDictionaryProcess>().EditArea(id, editName);

            return Json("修改成功！", district, JsonRequestBehavior.AllowGet);
        }

        [AjaxGet]
        public ActionResult DeleteArea()
        {
            var areaId = Guid.Parse(Request["Id"]);

            var sqlResult = ProcessInvoke.GetInstance<UserDictionaryProcess>().DeleteArea(areaId);

            sqlResult.Message = sqlResult.ErrorNumber == 547 
                ? "选中区域或选中区域的子区域已经存在关联酒店（饭店），请先删除关联酒店（饭店)后再删除此区域。" 
                : "删除成功！";

            var json = new JsonStruct
            {
                Success = sqlResult.Success,
                Message = sqlResult.Message
            };

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [AjaxGet]
        public ActionResult CateringEnterprise()
        {
            var page = string.IsNullOrWhiteSpace(Request["page"]) ? 1 : int.Parse(Request["page"]);

            var pageSize = string.IsNullOrWhiteSpace(Request["pageSize"]) ? 10 : int.Parse(Request["pageSize"]);

            var queryName = Request["queryName"];

            int count;

            var companyList = ProcessInvoke.GetInstance<CateringEnterpriseProcess>().GetPagedCateringCompanies(page, pageSize, queryName, out count);

            var model = new CateringEnterpriseViewModel
            {
                Count = count,
                PageSize = pageSize,
                QueryName = queryName,
                PageCount = (count % pageSize) > 0 ? (count / pageSize) + 1 : (count / pageSize),
                PageIndex = page,
                CateringCompanies = companyList
            };

            return View(model);
        }

        [AjaxGet]
        [HttpGet]
        public ActionResult EditCateringEnterprise(string guid)
        {
            if (string.IsNullOrWhiteSpace(guid))
            {
                return DefaultView();
            }

            var model = ProcessInvoke.GetInstance<CateringEnterpriseProcess>().GetCateringEnterprise(Guid.Parse(guid));
            return View(model);
        }

        [AjaxGet]
        [HttpPost]
        public ActionResult EditCateringEnterprise(CateringCompany model)
        {
            var propertyNames = Request.Form.AllKeys.Where(field => field != "Id" && field != "X-Requested-With").ToList();

            var exception = ProcessInvoke.GetInstance<CateringEnterpriseProcess>().AddOrUpdateCateringEnterprise(model, propertyNames);

            if (exception != null)
            {
                return View(model);
            }

            return RedirectToAction("SubmitSuccess", "Common",
                new { targetAction = "EditCateringEnterprise", targetcontroller = "Management", target = "slide-up-content", postform = "catering" });
        }

        [AjaxGet]
        [HttpGet]
        public ActionResult DeleteCateringEnterprise(Guid guid)
        {
            var success = ProcessInvoke.GetInstance<CateringEnterpriseProcess>().DeleteCateringEnterprise(guid);

            var json = new JsonStruct
            {
                Success = success,
                Message = !success ? "尝试删除餐饮企业信息失败，请刷新后重新尝试。" : "删除成功！",
                PostForm = "catering"
            };

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [AjaxGet]
        public ActionResult Hotel()
        {
            var page = string.IsNullOrWhiteSpace(Request["page"]) ? 1 : int.Parse(Request["page"]);

            var pageSize = string.IsNullOrWhiteSpace(Request["pageSize"]) ? 10 : int.Parse(Request["pageSize"]);

            var queryName = Request["queryName"];

            int count;

            var hotels = ProcessInvoke.GetInstance<HotelRestaurantProcess>().GetPagedHotelRestaurant(page, pageSize, queryName, out count);

            var model = new HotelViewModel()
            {
                Count = count,
                PageSize = pageSize,
                QueryName = queryName,
                PageCount = (count % pageSize) > 0 ? (count / pageSize) + 1 : (count / pageSize),
                PageIndex = page,
                HtoHotelRestaurants = hotels
            };

            return View(model);
        }

        [AjaxGet]
        [HttpGet]
        public ActionResult EditHotel(string guid)
        {
            GetHotelRelatedItems();

            if (string.IsNullOrWhiteSpace(guid))
            {
                return DefaultView();
            }

            var model = ProcessInvoke.GetInstance<HotelRestaurantProcess>().GetHotelRestaurant(Guid.Parse(guid));
            return View(model);
        }

        [AjaxGet]
        [HttpPost]
        public ActionResult EditHotel(HotelRestaurant model)
        {
            var propertyNames = Request.Form.AllKeys.Where(field => field != "Id" && field != "X-Requested-With").ToList();

            var exception = ProcessInvoke.GetInstance<HotelRestaurantProcess>().AddOrUpdateHotelRestaurant(model, propertyNames);

            if (exception != null)
            {
                GetHotelRelatedItems();
                return View(model);
            }

            return RedirectToAction("SubmitSuccess", "Common",
                new { targetAction = "EditHotel", targetcontroller = "Management", target = "slide-up-content", postform = "hotel" });
        }

        [AjaxGet]
        [HttpGet]
        public ActionResult DeleteHotel(Guid guid)
        {
            var success = ProcessInvoke.GetInstance<HotelRestaurantProcess>().DeleteHotelRestaurant(guid);

            var json = new JsonStruct
            {
                Success = success,
                Message = !success ? "尝试删除餐饮企业信息失败，请刷新后重新尝试。" : "删除成功！",
                PostForm = "catering"
            };

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        [AjaxGet]
        public ActionResult Device()
        {
            var page = string.IsNullOrWhiteSpace(Request["page"]) ? 1 : int.Parse(Request["page"]);

            var pageSize = string.IsNullOrWhiteSpace(Request["pageSize"]) ? 10 : int.Parse(Request["pageSize"]);

            var queryName = Request["queryName"];

            int count;

            var deviceList = ProcessInvoke.GetInstance<RestaurantDeviceProcess>().GetPagedRestaurantDevice(page, pageSize, queryName, out count);

            var model = new DeviceViewModel
            {
                Count = count,
                PageSize = pageSize,
                QueryName = queryName,
                PageCount = (count % pageSize) > 0 ? (count / pageSize) + 1 : (count / pageSize),
                PageIndex = page,
                RestaurantDevices = deviceList
            };

            return View(model);
        }

        [AjaxGet]
        [HttpGet]
        public ActionResult EditDevice(string guid)
        {
            GetDeviceRelatedItems();

            if (string.IsNullOrWhiteSpace(guid))
            {
                return DefaultView();
            }

            var model = ProcessInvoke.GetInstance<RestaurantDeviceProcess>().GetRestaurantDevice(Guid.Parse(guid));
            return View(model);
        }

        [AjaxGet]
        [HttpPost]
        public ActionResult EditDevice(RestaurantDevice model)
        {
            var propertyNames = Request.Form.AllKeys.Where(field => field != "Id" && field != "X-Requested-With").ToList();

            var exception = ProcessInvoke.GetInstance<RestaurantDeviceProcess>().AddOrUpdateRestaurantDevice(model, propertyNames);

            if (exception != null)
            {
                GetDeviceRelatedItems();
                return View(model);
            }

            return RedirectToAction("SubmitSuccess", "Common",
                new { targetAction = "EditDevice", targetcontroller = "Management", target = "slide-up-content", postform = "device" });
        }

        private void GetHotelRelatedItems()
        {
            ViewBag.CateringCompany = ProcessInvoke.GetInstance<CateringEnterpriseProcess>()
                .GetCateringCompanySelectList()
                .Select(obj => new SelectListItem() {Text = obj.Key, Value = obj.Value})
                .ToList();

            ViewBag.District = ProcessInvoke.GetInstance<UserDictionaryProcess>()
                .GetDistrictSelectList()
                .Select(obj => new SelectListItem() {Text = obj.Value, Value = obj.Key})
                .ToList();

            ViewBag.Street = new List<SelectListItem>();

            ViewBag.Address = new List<SelectListItem>();

            ViewBag.Status = new List<SelectListItem>()
            {
                new SelectListItem() {Text = "营业中", Value = "0"},
                new SelectListItem() {Text = "装修中", Value = "1"},
                new SelectListItem() {Text = "停业中", Value = "2"}
            };
        }

        private void GetDeviceRelatedItems()
        {
            ViewBag.Status = new List<SelectListItem>()
            {
                new SelectListItem() {Text = "使用中", Value = "0"},
                new SelectListItem() {Text = "停用中", Value = "1"},
                new SelectListItem() {Text = "维修中", Value = "2"}
            };

            ViewBag.CleanerType = new List<SelectListItem>()
            {
                new SelectListItem() {Text = "静电式", Value = "0"},
                new SelectListItem() {Text = "过滤式", Value = "1"},
                new SelectListItem() {Text = "负离子", Value = "2"},
                new SelectListItem() {Text = "光电式", Value = "3"}
            };
        }
    }
}
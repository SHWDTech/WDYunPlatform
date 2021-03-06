﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Lampblack_Platform.Common;
using Lampblack_Platform.Models.Management;
using MvcWebComponents.Attributes;
using MvcWebComponents.Controllers;
using MvcWebComponents.Filters;
using MvcWebComponents.Model;
using Platform.Process.Process;
using SHWDTech.Platform.Model.Model;

namespace Lampblack_Platform.Controllers
{
    [AjaxGet]
    public class ManagementController : WdControllerBase
    {
        public ActionResult Area() 
            => View();

        [NamedAuth(Modules = "Area")]
        public ActionResult GetAreaInfo()
        {
            var areaInfo = ProcessInvoke<UserDictionaryProcess>().GetAreaInfo();
            return Json(areaInfo, JsonRequestBehavior.AllowGet);
        }

        [NamedAuth(Modules = "Area")]
        public ActionResult GetAreaList()
        {
            var areaId = Request["id"];
            if (string.IsNullOrWhiteSpace(areaId)) return null;
            var parent = Guid.Parse(areaId);

            var list = ProcessInvoke<UserDictionaryProcess>().GetChildDistrict(parent);

            return Json(new JsonStruct() {
                Result = list.Select(obj => new {Id = obj.Key, ItemValue = obj.Value})}, 
                JsonRequestBehavior.AllowGet);
        }

        [NamedAuth(Modules = "Area")]
        public ActionResult AddAreaInfo()
        {
            var areaName = Request["areaName"];

            var itemLevel = int.Parse(Request["itemLevel"]);

            Guid parentNode;

            Guid.TryParse(Request["parentNode"], out parentNode);

            var district = ProcessInvoke<UserDictionaryProcess>().AddArea(areaName, itemLevel, parentNode);

            return district == null
                ? Json(new JsonStruct() { Message = "添加区县信息失败，请重新尝试。" }, JsonRequestBehavior.AllowGet)
                : Json("添加成功！", district, JsonRequestBehavior.AllowGet);
        }

        [NamedAuth(Modules = "Area")]
        public ActionResult EditAreaInfo()
        {
            var id = Guid.Parse(Request["itemId"]);
            var editName = Request["editName"];
            var district = ProcessInvoke<UserDictionaryProcess>().EditArea(id, editName);

            return Json("修改成功！", district, JsonRequestBehavior.AllowGet);
        }

        [NamedAuth(Modules = "Area")]
        public ActionResult DeleteArea()
        {
            var areaId = Guid.Parse(Request["Id"]);

            var sqlResult = ProcessInvoke<UserDictionaryProcess>().DeleteArea(areaId);

            sqlResult.Message = sqlResult.ErrorNumber == 547 
                ? "选中区域或选中区域的子区域已经存在关联酒店（饭店），请先删除关联酒店（饭店)后再删除此区域。" 
                : "删除成功！";

            if (sqlResult.ErrorNumber == 547)
            {
                HttpContext.Response.StatusCode = 500;
            }

            var json = new JsonStruct
            {
                Message = sqlResult.Message
            };

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CateringEnterprise()
        {
            var page = string.IsNullOrWhiteSpace(Request["page"]) ? 1 : int.Parse(Request["page"]);

            var pageSize = string.IsNullOrWhiteSpace(Request["pageSize"]) ? 10 : int.Parse(Request["pageSize"]);

            var queryName = Request["queryName"];

            int count;

            var companyList = ProcessInvoke<CateringEnterpriseProcess>().GetPagedCateringCompanies(page, pageSize, queryName, out count);

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

        [HttpGet]
        [NamedAuth(Modules = "CateringEnterprise")]
        public ActionResult EditCateringEnterprise(string guid)
        {
            if (string.IsNullOrWhiteSpace(guid))
            {
                return DefaultView();
            }

            var model = ProcessInvoke<CateringEnterpriseProcess>().GetCateringEnterprise(Guid.Parse(guid));
            return View(model);
        }

        [HttpPost]
        [NamedAuth(Modules = "CateringEnterprise")]
        public ActionResult EditCateringEnterprise(CateringCompany model)
        {
            var propertyNames = Request.Form.AllKeys.Where(field => field != "Id" && field != "X-Requested-With").ToList();

            var exception = ProcessInvoke<CateringEnterpriseProcess>().AddOrUpdateCateringEnterprise(model, propertyNames);

            if (exception != null)
            {
                return View(model);
            }

            return RedirectToAction("SubmitSuccess", "Common",
                new { targetAction = "EditCateringEnterprise", targetcontroller = "Management", target = "slide-up-content", postform = "catering" });
        }

        [HttpGet]
        [NamedAuth(Modules = "CateringEnterprise")]
        public ActionResult DeleteCateringEnterprise(Guid guid)
        {
            var success = ProcessInvoke<CateringEnterpriseProcess>().DeleteCateringEnterprise(guid);

            var json = new JsonStruct
            {
                Message = !success ? "尝试删除餐饮企业信息失败，请刷新后重新尝试。" : "删除成功！",
                PostForm = "catering"
            };

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Hotel()
        {
            var page = string.IsNullOrWhiteSpace(Request["page"]) ? 1 : int.Parse(Request["page"]);

            var pageSize = string.IsNullOrWhiteSpace(Request["pageSize"]) ? 10 : int.Parse(Request["pageSize"]);

            var queryName = Request["queryName"];

            int count;

            var hotels = ProcessInvoke<HotelRestaurantProcess>().GetPagedHotelRestaurant(page, pageSize, queryName, out count);

            var model = new HotelViewModel()
            {
                Count = count,
                PageSize = pageSize,
                QueryName = queryName,
                PageCount = (count % pageSize) > 0 ? (count / pageSize) + 1 : (count / pageSize),
                PageIndex = page,
                HotelRestaurants = hotels
            };

            return View(model);
        }

        [HttpGet]
        [NamedAuth(Modules = "Hotel")]
        public ActionResult EditHotel(string guid)
        {
            GetHotelRelatedItems();

            if (string.IsNullOrWhiteSpace(guid))
            {
                return DefaultView();
            }

            var model = ProcessInvoke<HotelRestaurantProcess>().GetHotelRestaurant(Guid.Parse(guid));
            return View(model);
        }

        [HttpPost]
        [NamedAuth(Modules = "Hotel")]
        public ActionResult EditHotel(HotelRestaurant model)
        {
            var propertyNames = Request.Form.AllKeys.Where(field => field != "Id" && field != "X-Requested-With").ToList();

            var exception = ProcessInvoke<HotelRestaurantProcess>().AddOrUpdateHotelRestaurant(model, propertyNames);

            if (exception != null)
            {
                GetHotelRelatedItems();
                return View(model);
            }

            return RedirectToAction("SubmitSuccess", "Common",
                new { targetAction = "EditHotel", targetcontroller = "Management", target = "slide-up-content", postform = "hotel" });
        }

        [HttpGet]
        [NamedAuth(Modules = "Hotel")]
        public ActionResult DeleteHotel(Guid guid)
        {
            var success = ProcessInvoke<HotelRestaurantProcess>().DeleteHotelRestaurant(guid);

            var json = new JsonStruct
            {
                Message = !success ? "尝试删除餐饮企业信息失败，请刷新后重新尝试。" : "删除成功！",
                PostForm = "catering"
            };

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Device()
        {
            var page = string.IsNullOrWhiteSpace(Request["page"]) ? 1 : int.Parse(Request["page"]);

            var pageSize = string.IsNullOrWhiteSpace(Request["pageSize"]) ? 10 : int.Parse(Request["pageSize"]);

            var queryName = Request["queryName"];

            int count;

            var deviceList = ProcessInvoke<RestaurantDeviceProcess>().GetPagedRestaurantDevice(page, pageSize, queryName, out count);

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

        [HttpGet]
        [NamedAuth(Modules = "Device")]
        public ActionResult EditDevice(string guid)
        {
            GetDeviceRelatedItems();

            if (string.IsNullOrWhiteSpace(guid))
            {
                return DefaultView();
            }

            var model = ProcessInvoke<RestaurantDeviceProcess>().GetRestaurantDevice(Guid.Parse(guid));
            return View(model);
        }

        [HttpPost]
        [NamedAuth(Modules = "Device")]
        public ActionResult EditDevice(RestaurantDevice model)
        {
            var propertyNames = Request.Form.AllKeys.Where(field => field != "Id" && field != "X-Requested-With").ToList();

            if (model.Id == Guid.Empty)
            {
                InitDeviceModel(model, propertyNames);
            }

            var exception = ProcessInvoke<RestaurantDeviceProcess>().AddOrUpdateRestaurantDevice(model, propertyNames);

            if (exception != null)
            {
                GetDeviceRelatedItems();
                return View(model);
            }

            return RedirectToAction("SubmitSuccess", "Common",
                new { targetAction = "EditDevice", targetcontroller = "Management", target = "slide-up-content", postform = "device" });
        }

        [NamedAuth(Modules = "Device")]
        public ActionResult DeleteDevice(Guid guid)
        {
            var success = ProcessInvoke<RestaurantDeviceProcess>().DeleteRestaurantDevice(guid);

            var json = new JsonStruct
            {
                Message = !success ? "尝试删除油烟设备信息失败，请刷新后重新尝试。" : "删除成功！",
                PostForm = "device"
            };

            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeviceMaintenance(DeviceMaintenaceViewModel model)
        {
            int count;
            var deviceMaintenances = ProcessInvoke<DeviceMaintenanceProcess>()
                .GetPagedDeviceMaintenance(model.PageIndex, model.PageSize, model.QueryName, out count);

            model.Count = count;
            model.DeviceMaintenances = deviceMaintenances;
            model.PageCount = (count%model.PageSize) > 0 ? (count/model.PageSize) + 1 : (count/model.PageSize);

            return View(model);
        }

        [HttpGet]
        [NamedAuth(Modules = "DeviceMaintenance")]
        public ActionResult EditDeviceMaintenance(string guid)
        {
            GetDeviceMaintenanceItems();

            if (string.IsNullOrWhiteSpace(guid))
            {
                return DefaultView();
            }

            var model = ProcessInvoke<DeviceMaintenanceProcess>().GetDeviceMaintenance(Guid.Parse(guid));
            return View(model);
        }

        [HttpPost]
        [NamedAuth(Modules = "DeviceMaintenance")]
        public ActionResult EditDeviceMaintenance(DeviceMaintenance model)
        {
            var propertyNames = Request.Form.AllKeys.Where(field => field != "Id" && field != "X-Requested-With").ToList();

            var exception = ProcessInvoke<DeviceMaintenanceProcess>().AddOrUpdateDeviceMaintenance(model, propertyNames);

            if (exception != null)
            {
                GetDeviceMaintenanceItems();
                return View(model);
            }

            return RedirectToAction("SubmitSuccess", "Common",
                new { targetAction = "EditDeviceMaintenance", targetcontroller = "Management", target = "slide-up-content", postform = "deviceMaintenance" });
        }

        private void GetHotelRelatedItems()
        {
            ViewBag.CateringCompany = ProcessInvoke<CateringEnterpriseProcess>()
                .GetCateringCompanySelectList()
                .Select(obj => new SelectListItem() { Text = obj.Value, Value = obj.Key.ToString() })
                .ToList();

            ViewBag.District = ProcessInvoke<UserDictionaryProcess>()
                .GetDistrictSelectList()
                .Select(obj => new SelectListItem() { Text = obj.Value, Value = obj.Key.ToString() })
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

            ViewBag.Hotel = ProcessInvoke<HotelRestaurantProcess>()
                .GetHotelRestaurantSelectList()
                .Select(obj => new SelectListItem() { Text = obj.Value, Value = obj.Key.ToString() })
                .ToList();

            ViewBag.DeviceModels = ProcessInvoke<DeviceModelProcess>()
                .GetDeviceModelSelectList()
                .Select(obj => new SelectListItem() { Text = obj.Value, Value = obj.Key.ToString() })
                .ToList();
        }

        private void GetDeviceMaintenanceItems()
        {
            ViewBag.Status = new List<SelectListItem>()
            {
                new SelectListItem {Text = "很脏", Value = "0"},
                new SelectListItem {Text = "一般", Value = "1"},
                new SelectListItem {Text = "干净", Value = "2"}
            };

            ViewBag.Users = ProcessInvoke<LampblackUserProcess>()
                .GetLampblackUserSelectList()
                .Select(obj => new SelectListItem() { Text = obj.Value, Value = obj.Key.ToString() })
                .ToList();

            ViewBag.Devices = ProcessInvoke<RestaurantDeviceProcess>()
                .GetRestaurantDeviceSelectList()
                .Select(obj => new SelectListItem() { Text = obj.Key.ToString(), Value = obj.Value })
                .ToList();

        }

        /// <summary>
        /// 初始化设备相关信息
        /// </summary>
        /// <param name="model"></param>
        /// <param name="properties"></param>
        private void InitDeviceModel(RestaurantDevice model, List<string> properties)
        {
            model.DeviceTypeId = LampblackConfig.DeviceTypeGuid;
            properties.Add("DeviceTypeId");
            model.FirmwareSetId = LampblackConfig.FirmwareSetGuid;
            properties.Add("FirmwareSetId");
        }
    }
}
using System;
using System.Web.Mvc;
using Lampblack_Platform.Models.Management;
using MvcWebComponents.Controllers;
using MvcWebComponents.Filters;
using MvcWebComponents.Model;
using Platform.Process;
using Platform.Process.Process;

namespace Lampblack_Platform.Controllers
{
    public class ManagementController : WdControllerBase
    {
        [AjaxGet]
        public ActionResult Area()
        {
            return View();
        }

        [AjaxGet]
        public ActionResult GetAreaInfo()
        {
            var areaInfo = ProcessInvoke.GetInstance<UserDictionaryProcess>().GetAreaInfo();
            return Json(areaInfo, JsonRequestBehavior.AllowGet);
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
            var itemKey = Guid.Parse(Request["Id"]);

            var success = ProcessInvoke.GetInstance<UserDictionaryProcess>().DeleteArea(itemKey);

            var json = new JsonStruct
            {
                Success = success,
                Message = !success ? "尝试删除区域信息失败，请刷新后重新尝试。" : "删除成功！"
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

        [HttpGet]
        public ActionResult EditCateringEnterprise()
        {
            return DefaultView();
        }

        [AjaxGet]
        [HttpPost]
        public ActionResult EditCateringEnterprise(EditCateringEnterpriseViewModel model)
        {
            var exception = ProcessInvoke.GetInstance<CateringEnterpriseProcess>().AddOrUpdateCateringEnterprise(model);

            if (exception != null)
            {
                return View(model);
            }

            return RedirectToAction("SubmitSuccess", "Common", new { targetAction = "EditCateringEnterprise", targetcontroller = "Management", target = "edit-container" });
        }

        [AjaxGet]
        public ActionResult Hotel()
        {
            return View();
        }

        [AjaxGet]
        public ActionResult Device()
        {
            return View();
        }
    }
}
using System.Web.Mvc;
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

            var parentNode = Request["parentNode"];

            var district = ProcessInvoke.GetInstance<UserDictionaryProcess>().AddArea(areaName, itemLevel, parentNode);

            return district == null 
                ? Json(new JsonStruct() {Success = false, Message = "添加区县信息失败，请重新尝试。"}, JsonRequestBehavior.AllowGet) 
                : Json("添加成功！", district, JsonRequestBehavior.AllowGet);
        }

        [AjaxGet]
        public ActionResult DeleteArea()
        {
            var itemKey = Request["ItemKey"];

            var success = ProcessInvoke.GetInstance<UserDictionaryProcess>().DeleteArea(itemKey);

            var json = new JsonStruct
            {
                Success = success,
                Message = !success ? "尝试删除区域信息失败，请刷新后重新尝试。" : "删除成功！"
            };


            return Json(json, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CateringEnterprise()
        {
            return View();
        }

        public ActionResult Hotel()
        {
            return View();
        }

        public ActionResult Device()
        {
            return View();
        }
    }
}
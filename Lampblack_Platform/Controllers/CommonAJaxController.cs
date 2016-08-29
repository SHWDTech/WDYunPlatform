using System;
using System.Linq;
using System.Web.Mvc;
using MvcWebComponents.Attributes;
using MvcWebComponents.Controllers;
using MvcWebComponents.Model;
using Platform.Process;
using Platform.Process.Process;
using WebViewModels.Enums;
using WebViewModels.ViewModel;

namespace Lampblack_Platform.Controllers
{
    [NamedAuth(Modules = "Ignore")]
    public class CommonAjaxController : WdControllerBase
    {
        public ActionResult GetAreaList()
        {
            var areaId = Request["id"];
            if (string.IsNullOrWhiteSpace(areaId)) return Json(new JsonStruct(), JsonRequestBehavior.AllowGet);
            Guid guid;
            Guid.TryParse(areaId, out guid);
            if (guid != Guid.Empty)
            {
                var list = ProcessInvoke.GetInstance<UserDictionaryProcess>().GetChildDistrict(guid);

                return Json(new JsonStruct()
                {
                    Result = list.Select(obj => new { Id = obj.Key, ItemValue = obj.Value })
                },
                    JsonRequestBehavior.AllowGet);
            }

            return Json(new JsonStruct(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Hotels()
        {
            var districtGuid = Guid.Empty;

            if (!string.IsNullOrWhiteSpace(Request["area"]))
            {
                districtGuid = Guid.Parse(Request["area"]);
            }

            if (!string.IsNullOrWhiteSpace(Request["street"]))
            {
                Guid guid;
                Guid.TryParse(Request["street"], out guid);
                if (guid != Guid.Empty)
                {
                    districtGuid = guid;
                }
            }

            if (!string.IsNullOrWhiteSpace(Request["address"]))
            {
                Guid guid;
                Guid.TryParse(Request["address"], out guid);
                if (guid != Guid.Empty)
                {
                    districtGuid = guid;
                }
            }

            var hotels = ProcessInvoke.GetInstance<HotelRestaurantProcess>().GetHotels(districtGuid);

            return Json(new JsonStruct()
            {
                Result = hotels.Select(obj => new { obj.Id, Name = obj.ProjectName })
            },
                JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTrendAnalysis(TrendAnalisysViewModel model)
        {
            if (model.ReportType != ReportType.Month)
            {
                model.StartDateTime = DateTime.Parse($"{Request["StartDateTime"]}-1-1");
                model.DueDateTime = DateTime.Parse($"{Request["DueDateTime"]}-1-1");
            }
            var result = ProcessInvoke.GetInstance<RunningTimeProcess>().GetRunningTimeReport(model);
            return Json(new JsonStruct()
            {
                Result = result.Select(obj => new { Date = obj.Key, Linkage = obj.Value })
            },
                JsonRequestBehavior.AllowGet);
        }
    }
}
using System;
using System.Linq;
using System.Web.Mvc;
using MvcWebComponents.Attributes;
using MvcWebComponents.Controllers;
using MvcWebComponents.Model;
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
            var list = ProcessInvoke<UserDictionaryProcess>().GetChildDistrict(guid);
            list.Add(Guid.Empty, "全部");
        
            return Json(new JsonStruct
            {
                Result = list.Select(obj => new { id = obj.Key, text = obj.Value }).ToList().OrderBy(o => o.id)
            },
                JsonRequestBehavior.AllowGet);
        }

        public ActionResult UserDistrictSelections()
        {
            var dict = ProcessInvoke<UserDictionaryProcess>().GetUserDistricts();
            dict.Add(Guid.Empty, "全部");

            return Json(new JsonStruct
            {
                Result = dict.Select(item => new { id = item.Key, text = item.Value }).ToList().OrderBy(o => o.id)
            }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult UserDistricts() => Json(new JsonStruct
        {
            Result = ProcessInvoke<UserDictionaryProcess>().GetUserDistricts().Select(item => new { id = item.Key, text = item.Value })
        }, JsonRequestBehavior.AllowGet);

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

            var hotels = ProcessInvoke<HotelRestaurantProcess>().GetHotels(districtGuid);

            return Json(new JsonStruct
            {
                Result = hotels.Select(obj => new { obj.Id, Name = obj.ProjectName })
            },
                JsonRequestBehavior.AllowGet);
        }

        public ActionResult Devices()
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

            var devs = ProcessInvoke<RestaurantDeviceProcess>().GetDevices(districtGuid);

            return Json(new JsonStruct
            {
                Result = devs.Select(obj => new { obj.Id, Name = obj.DeviceName })
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
            var result = ProcessInvoke<RunningTimeProcess>().GetRunningTimeReport(model);
            return Json(new JsonStruct
            {
                Result = result.Select(obj => new { Date = obj.Key, Linkage = obj.Value })
            },
                JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDistrictHotel(Guid id)
        {
            var hotels = ProcessInvoke<HotelRestaurantProcess>()
                .GetDistrictHotelRestaurants(id)
                .Select(obj => new { id = obj.Id, text = obj.ProjectName }).ToList();
            hotels.Add(new {id = Guid.Empty, text = "全部"});
            var ret = hotels.OrderBy(item => item.id);

            return Json(new JsonStruct
            {
                Result = ret
            },
                JsonRequestBehavior.AllowGet);
        }
    }
}
using System;
using System.Linq;
using System.Web.Mvc;
using Lampblack_Platform.Models.Home;
using MvcWebComponents.Attributes;
using MvcWebComponents.Controllers;
using MvcWebComponents.Filters;
using MvcWebComponents.Model;
using Platform.Process.Enums;
using Platform.Process.Process;

namespace Lampblack_Platform.Controllers
{
    [NamedAuth(Modules = "Ignore")]
    public class HomeController : WdControllerBase
    {
        public ActionResult Index()
        {
            var model = new IndexViewModel();

            var rates = ProcessInvoke<HotelRestaurantProcess>().GetHotelCleanessList();
            if (WdContext.UserDistricts != null)
            {
                rates = rates.Where(obj => WdContext.UserDistricts.Contains(obj.DistrictGuid)).ToList();
            }

            foreach (var rate in rates)
            {
                model.HotelCleanessList.Add(new HotelCleaness { HotelName = rate.ProjectName, CleanessRate = rate.ProjectCleaness });
            }

            model.NoData = rates.Count(obj => obj.ProjectCleaness == CleanessRateResult.NoData);

            model.Faild = rates.Count(obj => obj.ProjectCleaness == CleanessRateResult.Fail);

            model.Worse = rates.Count(obj => obj.ProjectCleaness == CleanessRateResult.Worse);

            model.Qualified = rates.Count(obj => obj.ProjectCleaness == CleanessRateResult.Qualified);

            model.Good = rates.Count(obj => obj.ProjectCleaness == CleanessRateResult.Good);

            ViewBag.Areas = ProcessInvoke<UserDictionaryProcess>().GetDistrictSelectList();

            return View(model);
        }

        [AjaxGet]
        public ActionResult HotelCurrentStatus(Guid hotelGuid)
        {
            var currentStatus = new IndexHotelCurrentViewModel(ProcessInvoke<HotelRestaurantProcess>().GetHotelCurrentStatus(hotelGuid));

            return Json(new JsonStruct
            {
                Result = currentStatus
            }, JsonRequestBehavior.AllowGet);
        }

        [AjaxGet]
        public ActionResult DeviceCurrentStatus(Guid hotelGuid)
        {
            var currentStatus = ProcessInvoke<RestaurantDeviceProcess>().GetDeviceCurrentStatus(hotelGuid);

            return Json(new JsonStruct
            {
                Result = currentStatus
            }, JsonRequestBehavior.AllowGet);
        }
    }
}
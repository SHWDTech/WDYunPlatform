using System.Web.Mvc;
using MvcWebComponents.Controllers;
using MvcWebComponents.Filters;

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
        public ActionResult AddDistrict()
        {

            return Json("");
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
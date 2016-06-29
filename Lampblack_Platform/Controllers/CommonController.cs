using System.Web.Mvc;
using MvcWebComponents.Controllers;

namespace Lampblack_Platform.Controllers
{
    public class CommonController : WdControllerBase
    {
        // GET: Common
        public ActionResult SubmitSuccess()
        {
            ViewBag.TargetAction = Request["targetAction"];
            ViewBag.TargetController = Request["targetcontroller"];
            ViewBag.Target = Request["target"];

            return View();
        }
    }
}
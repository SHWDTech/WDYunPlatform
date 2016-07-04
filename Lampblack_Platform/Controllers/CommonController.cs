using System.Web.Mvc;
using MvcWebComponents.Attributes;
using MvcWebComponents.Controllers;

namespace Lampblack_Platform.Controllers
{
    [NamedAuth(Modules = "Ignore")]
    public class CommonController : WdControllerBase
    {
        // GET: Common
        public ActionResult SubmitSuccess()
        {
            ViewBag.TargetAction = Request["targetAction"];
            ViewBag.TargetController = Request["targetcontroller"];
            ViewBag.Target = Request["target"];
            ViewBag.PostForm = Request["postform"];

            return View();
        }
    }
}
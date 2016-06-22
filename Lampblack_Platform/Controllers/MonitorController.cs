using System.Web.Mvc;
using MvcWebComponents.Controllers;

namespace Lampblack_Platform.Controllers
{
    public class MonitorController : WdControllerBase
    {
        [HttpGet]
        public ActionResult Map()
        {
            return View();
        }
    }
}
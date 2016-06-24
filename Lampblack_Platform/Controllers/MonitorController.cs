using System.Web.Mvc;
using MvcWebComponents.Controllers;
using MvcWebComponents.Filters;

namespace Lampblack_Platform.Controllers
{
    public class MonitorController : WdControllerBase
    {
        [AjaxGet]
        public ActionResult Map()
        {
            return PartialView();
        }
    }
}
using System.Web.Mvc;
using MvcWebComponents.Controllers;
using MvcWebComponents.Filters;

namespace Lampblack_Platform.Controllers
{
    [AjaxGet]
    public class SummaryController : WdControllerBase
    {
        // GET: Summary
        public ActionResult GeneralSummary()
        {
            return View();
        }
    }
}
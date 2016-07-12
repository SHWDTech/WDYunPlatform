using System.Web.Mvc;
using MvcWebComponents.Controllers;
using MvcWebComponents.Filters;

namespace Lampblack_Platform.Controllers
{
    [AjaxGet]
    public class QueryController : WdControllerBase
    {
        // GET: Query
        public ActionResult CleanRate()
        {
            return View();
        }

        public ActionResult LinkageRate()
        {
            return View();
        }

        public ActionResult RemovalRate()
        {
            return View();
        }

        public ActionResult Alarm()
        {
            return View();
        }

        public ActionResult HistoryData()
        {
            return View();
        }

        public ActionResult RunningTime()
        {
            return View();
        }
    }
}
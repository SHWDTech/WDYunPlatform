using System.Web.Mvc;
using MvcWebComponents.Controllers;
using MvcWebComponents.Filters;

namespace Lampblack_Platform.Controllers
{
    [AjaxGet]
    public class AnalysisController : WdControllerBase
    {
        // GET: Analysis
        public ActionResult ExceptionData()
        {
            return View();
        }

        public ActionResult RunningStatus()
        {
            return View();
        }

        public ActionResult GeneralReport()
        {
            return View();
        }

        public ActionResult GeneralComparison()
        {
            return View();
        }

        public ActionResult TrendAnalysis()
        {
            return View();
        }

        public ActionResult CleanlinessStatistics()
        {
            return View();
        }
    }
}
using System.Web.Mvc;
using MvcWebComponents.Controller;

namespace Lampblack_Platform.Controllers
{
    public class HomeController : WdControllerBase
    {
        public ActionResult Index() => View();

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
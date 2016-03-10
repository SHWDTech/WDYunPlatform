using System.Web.Mvc;

namespace SHWDTech.Web_Cloud_Platform.Controllers
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
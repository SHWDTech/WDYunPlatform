using System.Web.Mvc;
using MvcWebComponents.Controllers;

namespace Lampblack_Platform.Controllers
{
    public class ManagementController : WdControllerBase
    {
        // GET: Management
        public ActionResult Area()
        {
            return View();
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
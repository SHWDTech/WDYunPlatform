using System.Web.Mvc;
using Lampblack_Platform.Models.Home;
using MvcWebComponents.Attributes;
using MvcWebComponents.Controllers;

namespace Lampblack_Platform.Controllers
{
    [NamedAuth(Modules = "Ignore")]
    public class HomeController : WdControllerBase
    {
        public ActionResult Index()
        {
            var model = new IndexViewModel();


            return View(model);
        }
    }
}
using System.Web.Mvc;
using MvcWebComponents.Attributes;
using MvcWebComponents.Controllers;

namespace Lampblack_Platform.Controllers
{
    [NamedAuth(Modules = "Ignore")]
    public class HomeController : WdControllerBase
    {
        public ActionResult Index() => View();
    }
}
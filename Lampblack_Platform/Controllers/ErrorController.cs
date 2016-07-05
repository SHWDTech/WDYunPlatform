using System.Web.Mvc;
using MvcWebComponents.Attributes;
using MvcWebComponents.Controllers;

namespace Lampblack_Platform.Controllers
{
    [NamedAuth(Modules = "Ignore")]
    public class ErrorController : WdControllerBase
    {
        // GET: Error
        public ActionResult UnAuthorized() 
            => Request.IsAjaxRequest() ? PartialView() : View();
    }
}
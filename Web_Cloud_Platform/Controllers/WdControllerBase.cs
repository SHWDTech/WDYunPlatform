using System.Linq;
using System.Web.Mvc;
using SHWDTech.Web_Cloud_Platform.Common;

namespace SHWDTech.Web_Cloud_Platform.Controllers
{
    public class WdControllerBase : Controller
    {
        public WdContext WdContext { get; }

        public WdControllerBase()
        {
            WdContext = (WdContext)HttpContext;
        }

        protected override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionDescriptor.ActionName == "Login")
            {
                base.OnActionExecuting(context);
                return;
            }
            
        }
    }
}
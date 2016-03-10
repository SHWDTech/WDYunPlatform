using System.Web.Mvc;
using Platform.Process.Process;
using SHWDTech.Web_Cloud_Platform.Common;

namespace SHWDTech.Web_Cloud_Platform.Controllers
{
    public class WdControllerBase : Controller
    {
        public WdContext WdContext { get; }

        private readonly ControllerProcess _controllerProcess;

        public WdControllerBase()
        {
            WdContext = (WdContext)HttpContext;
            _controllerProcess = new ControllerProcess();
        }

        protected override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionDescriptor.ActionName == "Login")
            {
                base.OnActionExecuting(context);
                return;
            }

            WdContext.WdUser = _controllerProcess.GetCurrentUser(WdContext.HttpContext);
        }
    }
}
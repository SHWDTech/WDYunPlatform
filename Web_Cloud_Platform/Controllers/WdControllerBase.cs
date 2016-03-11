using Platform.Process.Process;
using SHWDTech.Web_Cloud_Platform.Common;
using System.Web.Mvc;

namespace SHWDTech.Web_Cloud_Platform.Controllers
{
    /// <summary>
    /// 系统控制器基类
    /// </summary>
    public class WdControllerBase : Controller
    {
        /// <summary>
        /// 控制器上下文信息
        /// </summary>
        public WdContext WdContext { get; set; }

        /// <summary>
        /// 控制器处理类
        /// </summary>
        private readonly ControllerProcess _controllerProcess;

        public WdControllerBase()
        {
            _controllerProcess = new ControllerProcess();
        }

        protected override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionDescriptor.ActionName == "Login")
            {
                base.OnActionExecuting(context);
                return;
            }

            WdContext = (WdContext)HttpContext;

            WdContext.WdUser = _controllerProcess.GetCurrentUser(WdContext.HttpContext);
        }
    }
}
using System.Threading;
using System.Web.Mvc;
using Platform.Process.Process;
using SHWD.Platform.Repository.IRepository;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Web_Cloud_Platform.Common;

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
        public WdContext WdContext { get; }

        /// <summary>
        /// 控制器处理类
        /// </summary>
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
            RepositoryBase.ContextLocal = new ThreadLocal<IRepositoryContext>()
            {
                
            };
           
        }
    }
}
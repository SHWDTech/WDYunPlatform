using Platform.Process.Process;
using SHWD.Platform.Repository.IRepository;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Web_Cloud_Platform.Common;
using System.Threading;
using System.Web;
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

            SetApplicationContext(System.Web.HttpContext.Current);
            base.OnActionExecuting(context);
        }

        /// <summary>
        /// 设置程序所需的上下文
        /// </summary>
        /// <param name="context">HTTP上下文信息</param>
        private void SetApplicationContext(HttpContext context)
        {
            WdContext = new WdContext(context);
            WdContext.WdUser = _controllerProcess.GetCurrentUser(WdContext.HttpContext);

            //设置数据仓库类当前线程所需的用户和用户所属域信息
            RepositoryBase.ContextLocal = new ThreadLocal<IRepositoryContext>
            {
                Value = new RepositoryContext
                {
                    CurrentUser = WdContext.WdUser,
                    CurrentDomain = WdContext.WdUser.Domain
                }
            };
        }
    }
}
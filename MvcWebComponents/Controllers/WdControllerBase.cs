using System.Threading;
using SHWD.Platform.Repository.Repository;
using System.Web;
using System.Web.Mvc;
using MvcWebComponents.Model;
using SHWD.Platform.Repository.IRepository;
using Platform.Process.Process;

namespace MvcWebComponents.Controllers
{
    public class WdControllerBase : Controller
    {
        /// <summary>
        /// 控制器处理程序
        /// </summary>
        private readonly ControllerProcess _controllerProcess;

        public WdControllerBase()
        {
            _controllerProcess = new ControllerProcess();
        }

        /// <summary>
        /// 控制器上下文信息
        /// </summary>
        protected WdContext WdContext { get; set; }

        protected override void OnActionExecuting(ActionExecutingContext ctx)
        {
            if (ctx.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
                || ctx.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                base.OnActionExecuting(ctx);
                return;
            }

            SetActionContext(System.Web.HttpContext.Current);
        }

        private void SetActionContext(HttpContext context)
        {
            var currentUser = _controllerProcess.GetCurrentUser(context);
            WdContext = new WdContext(currentUser);

            RepositoryBase.ContextLocal = new ThreadLocal<IRepositoryContext>()
            {
                Value = new RepositoryContext()
                {
                    CurrentUser = WdContext.WdUser,
                    CurrentDomain = WdContext.Domain
                }
            };
        }

        /// <summary>
        /// 创建一个将视图呈现给响应的ViewResult对象
        /// </summary>
        /// <returns></returns>
        protected new ViewResult View()
        {
            var model = new ViewModelBase()
            {
                Menus = WdContext.Modules
            };
            return View(model);
        }

        /// <summary>
        /// 创建一个将视图呈现给响应的ViewResult对象（使用默认视图方法）
        /// </summary>
        /// <returns></returns>
        protected ViewResult DefaultView() => base.View();

        /// <summary>
        /// 创建一个将视图呈现给响应的ViewResult对象（使用默认视图方法）
        /// </summary>
        /// <param name="model">视图模型</param>
        /// <returns></returns>
        protected ViewResult DefaultView(object model) => base.View(model);
    }
}

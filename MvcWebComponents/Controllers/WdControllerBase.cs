using System.Threading;
using SHWD.Platform.Repository.Repository;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
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
                return;
            }

            SetActionContext(ctx, System.Web.HttpContext.Current);
        }

        private void SetActionContext(ActionExecutingContext ctx, HttpContext context)
        {
            var currentUser = _controllerProcess.GetCurrentUser(context);

            if (currentUser == null)
            {
                FormsAuthentication.SignOut();
                ctx.Result = RedirectToAction("Login", "Account");
                return;
            }
            WdContext = new WdContext(currentUser);
            if (!context.Items.Contains("WdContext"))
            {
                context.Items.Add("WdContext", WdContext);
            }

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
        protected new ActionResult View()
        {
            if (Request.IsAjaxRequest()) return PartialView();

            var model = new ViewModelBase()
            {
                Context = WdContext
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

        /// <summary>
        /// 创建一个将指定对象序列化为 JavaScript 对象表示法 (JSON) 的对象。
        /// </summary>
        /// <param name="json"></param>
        /// <param name="requestBehavior"></param>
        /// <returns></returns>
        protected JsonResult Json(JsonStruct json, JsonRequestBehavior requestBehavior = JsonRequestBehavior.DenyGet)
            => base.Json(json, requestBehavior);

        /// <summary>
        /// 创建一个将指定对象序列化为 JavaScript 对象表示法 (JSON) 的对象。
        /// </summary>
        /// <param name="message"></param>
        /// <param name="requestBehavior"></param>
        /// <returns></returns>
        protected JsonResult Json(string message, JsonRequestBehavior requestBehavior = JsonRequestBehavior.DenyGet)
        {
            var json = new JsonStruct()
            {
                Success = true,
                Message = message,
            };

            return Json(json, requestBehavior);
        }

        /// <summary>
        /// 创建一个将指定对象序列化为 JavaScript 对象表示法 (JSON) 的对象。
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <param name="requestBehavior"></param>
        /// <returns></returns>
        protected JsonResult Json(string message, object data, JsonRequestBehavior requestBehavior = JsonRequestBehavior.DenyGet)
        {
            var json = new JsonStruct()
            {
                Success = true,
                Message = message,
                Result = data
            };

            return Json(json, requestBehavior);
        }

        /// <summary>
        /// 创建一个将指定对象序列化为 JavaScript 对象表示法 (JSON) 的对象。
        /// </summary>
        /// <param name="data"></param>
        /// <param name="requestBehavior"></param>
        /// <returns></returns>
        protected new JsonResult Json(object data, JsonRequestBehavior requestBehavior = JsonRequestBehavior.DenyGet)
        {
            var json = new JsonStruct()
            {
                Success = true,
                Result = data
            };

            return base.Json(json, requestBehavior);
        }
    }
}

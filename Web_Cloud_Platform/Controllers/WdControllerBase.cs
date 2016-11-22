using System.Web;
using System.Web.Mvc;
using Platform.Process.Process;
using Web_Cloud_Platform.Common;
using Web_Cloud_Platform.Models;

namespace Web_Cloud_Platform.Controllers
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
        }

        /// <summary>
        /// 设置基础模型类数据
        /// </summary>
        /// <param name="model"></param>
        protected void SetUpBasicViewModel(IBasicViewModel model)
        {
            var basePageInfo = _controllerProcess.GetBasePageInformation(WdContext.WdUser);

            model.CompanyInfomation.CompanyName = basePageInfo["CompanyName"];
            model.CompanyInfomation.PlatformName = basePageInfo["PlatformName"];
        }

        /// <summary>
        /// 处理页面通用数据模型，并使用模型创建一个将视图呈现给响应的ViewResult对象。
        /// </summary>
        /// <returns>视图呈现的模型。</returns>
        protected ActionResult DynamicView()
        {
            var baseModel = new BasicViewModel();

            return DynamicView(baseModel);
        }

        /// <summary>
        /// 处理页面通用数据模型，并使用模型创建一个将视图呈现给响应的ViewResult对象。
        /// </summary>
        /// <param name="model">需要使用的模型对象</param>
        /// <returns>视图呈现的模型。</returns>
        protected ActionResult DynamicView(IBasicViewModel model)
        {
            SetUpBasicViewModel(model);
            return DynamicView(null, null, model);
        }

        /// <summary>
        /// 处理页面通用数据模型，并使用模型创建一个将视图呈现给响应的ViewResult对象。
        /// </summary>
        /// <param name="viewName"></param>
        /// <param name="masterName"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        protected ActionResult DynamicView(string viewName, string masterName, object model)
        {
            if (model == null)
            {
                var basicModel = new BasicViewModel();
                SetUpBasicViewModel(basicModel);
                model = basicModel;
            }

            return View(viewName, masterName, model);
        }
    }
}
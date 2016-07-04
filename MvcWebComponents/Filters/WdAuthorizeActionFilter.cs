using System.Linq;
using System.Web.Mvc;
using MvcWebComponents.Controllers;
using MvcWebComponents.Model;

namespace MvcWebComponents.Filters
{
    public class WdAuthorizeActionFilter : IActionFilter
    {
        /// <summary>
        /// 当前请求上下文
        /// </summary>
        private WdContext WdContext { get; set; }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
                || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
                {
                    return;
                }

                WdContext = (WdContext)filterContext.HttpContext.Items["WdContext"];
                var modules = GetAuthModules(filterContext);

                if (!(modules.ActionRequired && modules.ControllerRequired)) return;

                if (WdContext.WdUser.IsInRole("Root")
                    || WdContext.WdUser.IsInRole("SuperAdmin")
                    || modules.ControllerModule == "Home"
                    || modules.ControllerModule == "Account")
                {
                    return;
                }

                var actionPermission = WdContext.Permissions.FirstOrDefault(obj => obj.PermissionName == modules.ActionModule);
                var controllerPermission =
                    WdContext.Permissions.FirstOrDefault(obj => obj.PermissionName == modules.ControllerModule);

                if (actionPermission == null || (actionPermission.ParentPermissionId != null && controllerPermission == null))
                {
                    filterContext.Result = new RedirectResult("/");
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        /// <summary>
        /// 获取当前执行方法的模块信息
        /// </summary>
        /// <param name="filterContext"></param>
        /// <returns></returns>
        private AuthModules GetAuthModules(ActionExecutingContext filterContext)
        {
            var controllerAuth =
                    filterContext.ActionDescriptor.ControllerDescriptor.GetCustomAttributes(typeof(NamedAuthAttribute),
                        true);
            var actionAuth =
                filterContext.ActionDescriptor.GetCustomAttributes((typeof(NamedAuthAttribute)), true);

            var controllerModule = controllerAuth.Length > 0
                ? ((NamedAuthAttribute)controllerAuth[0])
                : null;

            var actionModule = actionAuth.Length > 0
                ? ((NamedAuthAttribute) actionAuth[0])
                : null;

            return new AuthModules(controllerModule, actionModule);
        }
    }
}

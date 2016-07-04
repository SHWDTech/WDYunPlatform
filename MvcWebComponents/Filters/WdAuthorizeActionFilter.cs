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
                if (WdContext.WdUser.IsInRole("Root")
                    || WdContext.WdUser.IsInRole("SuperAdmin"))
                {
                    return;
                }

                var executer = new NamedAuthorizeExecuter(filterContext);

                if (!(executer.ActionRequired && executer.ControllerRequired)
                    || executer.ActionModule == "Ignore"
                    || (executer.ActionModule == string.Empty && executer.ControllerModule == "Ignore")) return;

                executer.AdjustModule(filterContext);

                var actionPermission = WdContext.Permissions.FirstOrDefault(obj => obj.PermissionName == executer.ActionModule);
                var controllerPermission =
                    WdContext.Permissions.FirstOrDefault(obj => obj.PermissionName == executer.ControllerModule);

                if (actionPermission == null || (actionPermission.ParentPermissionId != null && controllerPermission == null))
                {
                    filterContext.Result = new RedirectResult("/");
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }
    }
}

using System.Linq;
using System.Web.Mvc;
using MvcWebComponents.Controllers;

namespace MvcWebComponents.Filters
{
    public class WdAuthorizeActionFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
                || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
                {
                    return;
                }

                var controllerModules = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                var actionModules = filterContext.ActionDescriptor.ActionName;
                var wdContext = (WdContext)filterContext.HttpContext.Items["WdContext"];

                if (wdContext.WdUser.IsInRole("Root") 
                    || wdContext.WdUser.IsInRole("SuperAdmin") 
                    || controllerModules == "Home"
                    || controllerModules == "Account")
                {
                    return;
                }

                if (wdContext.Permissions.All(obj => obj.PermissionName != controllerModules || obj.PermissionName != actionModules))
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

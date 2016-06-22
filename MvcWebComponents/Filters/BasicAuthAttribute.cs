using System;
using System.Linq;
using System.Web.Mvc;
using MvcWebComponents.Controllers;

namespace MvcWebComponents.Filters
{
    /// <summary>
    /// 基本授权模块
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class BasicAuthAttribute : ActionFilterAttribute, IAuthorizationFilter
    {
        /// <summary>
        /// 模块名称
        /// </summary>
        public string Modules { get; set; }

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            var wdContext = (WdContext)filterContext.HttpContext.Items["WdContext"];

            if (!wdContext.WdUser.IsInRole("Root") &&  !wdContext.WdUser.IsInRole("Admin") && wdContext.Permissions.All(obj => obj.PermissionName != Modules))
            {
                filterContext.Result = new RedirectResult("");
            }
        }
    }
}

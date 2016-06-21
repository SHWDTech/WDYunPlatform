using System;
using System.Web.Mvc;

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
            var user = filterContext.HttpContext.User.Identity.Name;

            var controllerModule =
                filterContext.ActionDescriptor.ControllerDescriptor.GetFilterAttributes(true);
            foreach (var attribute in controllerModule)
            {
                if (attribute is BasicAuthAttribute)
                {
                    var attr = attribute as BasicAuthAttribute;
                    var modules = attr.Modules;
                }
            }
        }
    }
}

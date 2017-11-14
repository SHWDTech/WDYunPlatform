using System;
using System.Web.Mvc;

namespace MvcWebComponents.Filters
{
    /// <summary>
    /// 表示一个特性，该特性指示被标记的控制器或Action只响应Ajax请求或在模板中渲染
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AjaxGetAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// 初始化AjaxGet特性的新实例
        /// </summary>
        public AjaxGetAttribute()
        {
            
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if(filterContext.ActionDescriptor.IsDefined(typeof(NotAjaxGetAttribute), true)) return;
            if (!filterContext.IsChildAction && !filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new RedirectResult("/");
            }
        }
    }

    public class NotAjaxGetAttribute : ActionFilterAttribute
    {
        public NotAjaxGetAttribute()
        {

        }
    }
}

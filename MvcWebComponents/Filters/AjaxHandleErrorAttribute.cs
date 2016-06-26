using System;
using System.Web.Mvc;
using MvcWebComponents.Model;

namespace MvcWebComponents.Filters
{
    /// <summary>
    /// 表示一个特性，该特性指示被标记的控制器或Action在响应Ajax请求时发生异常后的处理方法。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AjaxHandleErrorAttribute : HandleErrorAttribute
    {
        /// <summary>
        /// 初始化AjaxHandleError特性的新实例
        /// </summary>
        public AjaxHandleErrorAttribute()
        {

        }

        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                var json = new JsonStruct()
                {
                    Success = false,
                    Message = "请求失败，请重新尝试，若多次失败请联系管理员！"
                };

                #if DEBUG
                json.Exception = filterContext.Exception; 
                #endif
                filterContext.Result = new JsonResult()
                {
                    Data = new JsonStruct()
                    {
                        Success = false,
                        Message = "请求失败，请重新尝试，若多次失败请联系管理员！"
                    }
                };
                filterContext.ExceptionHandled = true;
            }
        }
    }
}

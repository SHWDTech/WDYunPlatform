﻿using System;
using System.Web.Mvc;
using MvcWebComponents.Model;
using SHWDTech.Platform.Utility;

namespace MvcWebComponents.Filters
{
    /// <summary>
    /// 表示一个特性，该特性指示被标记的控制器或Action在响应Ajax请求时发生异常后的处理方法。
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AjaxHandleErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.HttpContext.Response.StatusCode = 500;
                var json = new JsonStruct()
                {
                    Message = "请求失败，请重新尝试，若多次失败请联系管理员！"
                };

                #if DEBUG
                json.Exception = filterContext.Exception.ToString(); 
                #endif

                LogService.Instance.Error("异步请求错误。", filterContext.Exception);
                filterContext.Result = new JsonResult()
                {
                    Data = json,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
                filterContext.ExceptionHandled = true;
            }
        }
    }
}

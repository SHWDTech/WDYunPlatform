﻿using System.Web.Mvc;
using MvcWebComponents.Model;

namespace MvcWebComponents.Attributes
{
    public class WdAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                filterContext.Result = new JsonResult()
                {
                    Data = new JsonStruct()
                    {
                        Success = false,
                        Message = "当前用户登录已经超时，请重新登陆！"
                    },
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}

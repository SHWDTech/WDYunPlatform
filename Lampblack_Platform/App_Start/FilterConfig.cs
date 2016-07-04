using System.Web.Mvc;
using MvcWebComponents.Attributes;
using MvcWebComponents.Filters;

namespace Lampblack_Platform
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new WdUnauthorizedAttribute());
            filters.Add(new WdAuthorizeActionFilter());
            filters.Add(new AjaxHandleErrorAttribute());
        }
    }
}

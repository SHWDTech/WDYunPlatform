using System.Web.Mvc;
using MvcWebComponents.Filters;

namespace Lampblack_Platform
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthorizeAttribute());
            filters.Add(new AjaxHandleErrorAttribute());
        }
    }
}

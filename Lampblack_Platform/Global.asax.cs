using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Platform.Process.Process;
using SHWD.Platform.Repository;

namespace Lampblack_Platform
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            DbRepository.ConnectionName = "Lampblack_Platform";
            GeneralProcess.LoadBaseInfomations();
        }
    }
}

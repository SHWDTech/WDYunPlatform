using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Platform.Cache;
using Platform.Process.Process;
using SHWD.Platform.Repository;
using SHWDTech.Platform.Model.Business;

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

            GlobalInitial();
        }

        /// <summary>
        /// 初始化网站全局信息
        /// </summary>
        private void GlobalInitial()
        {
            DbRepository.ConnectionName = "Lampblack_Platform";
            GeneralProcess.LoadBaseInfomations();

            var deviceModels = GeneralProcess.GetDeviceModels();
            foreach (var model in deviceModels)
            {
                var rate = new CleanessRate(model);
                PlatformCaches.Add($"CleanessRate-{model.Id}", rate, false, "deviceModels"); 
            }
        }
    }
}

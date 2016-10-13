using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Lampblack_Platform.Common;
using MvcWebComponents;
using MvcWebComponents.Controllers;
using Platform.Cache;
using Platform.Process;
using Platform.Process.Process;
using SHWD.Platform.Repository;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.Business;
using SHWDTech.Platform.Model.Model;

namespace Lampblack_Platform
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            GlobalInitial();
            SetRepositoryFilter();
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

            var configDictionary = new Dictionary<string, object>
            {
                {
                    "deviceTypeGuid",
                    ((IList<DeviceType>)
                        GeneralProcess.GetConfig<DeviceType>(obj => obj.DeviceTypeCode == "WD_Lampblack"))
                        .FirstOrDefault()?.Id
                },
                {
                    "firmwareSetGuid",
                    ((IList<FirmwareSet>)
                        GeneralProcess.GetConfig<FirmwareSet>(obj => obj.FirmwareSetName == "油烟协议第一版"))
                        .FirstOrDefault()?.Id
                }
            };

            LampblackConfig.InitConfig(configDictionary);
            WdControllerBase.LoginName = LampblackConfig.LoginName;
            PlatformCaches.Add("Cleaness", ProcessInvoke.GetInstance<HotelRestaurantProcess>().GetHotelCleanessList());

            var updateCleaness = new WdScheduler(SchedulerType.Interval)
            {
                Interval = 120000
            };
            updateCleaness.OnExecuting += () =>
            {
                PlatformCaches.Add("Cleaness", ProcessInvoke.GetInstance<HotelRestaurantProcess>().GetHotelCleanessList());
            };
            WdSchedulerManager.Register(updateCleaness);
        }

        private void SetRepositoryFilter()
        {
            if (string.IsNullOrWhiteSpace(LampblackConfig.District)) return;
            var districtId = Guid.Parse(LampblackConfig.District);
            UserDictionaryRepository.Filter = (obj => obj.Id == districtId
                                                      || (obj.ParentDictionary != null && obj.ParentDictionaryId == districtId)
                                                      || (obj.ParentDictionary != null && obj.ParentDictionary.ParentDictionary != null && obj.ParentDictionary.ParentDictionaryId == districtId));
            HotelRestaurantRepository.Filter = (obj => obj.DistrictId == districtId);
        }
    }
}

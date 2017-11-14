using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Lampblack_Platform.Common;
using Lampblack_Platform.Schedule;
using MvcWebComponents.Controllers;
using Platform.Cache;
using Platform.Process.Process;
using Quartz;
using Quartz.Impl;
using SHWD.Platform.Repository;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.Business;
using SHWDTech.Platform.Model.Model;
using WebViewModels.Enums;

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
            StartSchedu();
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

            PlatformCaches.Add("DistrictInfo", (List<UserDictionary>)GeneralProcess.GetConfig<UserDictionary>(o => o.ItemName == "Area" && o.ItemLevel == 0));

            var userDiscticts =
                (IList<SysDictionary>) GeneralProcess.GetConfig<SysDictionary>(c => c.ItemName == "userDistrict");
            PlatformCaches.Add("userDistrict", userDiscticts, false, "SystemConfig");

            LampblackConfig.InitConfig(configDictionary);
            WdControllerBase.LoginName = LampblackConfig.LoginName;
        }

        private static void SetRepositoryFilter()
        {
            if (string.IsNullOrWhiteSpace(LampblackConfig.District)) return;
            var districtId = Guid.Parse(LampblackConfig.District);
            UserDictionaryRepository.Filter = obj => obj.Id == districtId
                                                     || obj.ParentDictionary != null && obj.ParentDictionaryId == districtId
                                                     || obj.ParentDictionary != null && obj.ParentDictionary.ParentDictionary != null && obj.ParentDictionary.ParentDictionaryId == districtId;
            HotelRestaurantRepository.Filter = (obj => obj.DistrictId == districtId);
        }

        private static void StartSchedu()
        {
            var scheduler = StdSchedulerFactory.GetDefaultScheduler();

            scheduler.Start();

            var job = JobBuilder.Create<JinganFifteenDataPostJob>()
                .Build();

            var trigger = TriggerBuilder.Create()
                .StartAt(DateTime.Now.AddMinutes(15 - DateTime.Now.Minute % 15))
                .WithSimpleSchedule(x => x.WithIntervalInMinutes(15).RepeatForever())
                .Build();

            scheduler.ScheduleJob(job, trigger);

            var hourStatisJob = JobBuilder.Create<HourStatisticsJob>().Build();
            var commandDatas = new List<Guid>
            {
                CommandDataId.CleanerCurrent,
                CommandDataId.FanCurrent,
                CommandDataId.LampblackInCon,
                CommandDataId.LampblackOutCon
            };
            hourStatisJob.JobDataMap.Add("commandDatas", commandDatas);
            var hourStatisTrigger = TriggerBuilder.Create()
                //.StartAt(DateTime.Now.GetCurrentHour().AddHours(1).AddMinutes(2))
                .StartAt(DateTime.Now.AddSeconds(30))
                .WithSimpleSchedule(x => x.WithIntervalInHours(1).RepeatForever())
                .Build();

            scheduler.ScheduleJob(hourStatisJob, hourStatisTrigger);
            var dayStatisJob = JobBuilder.Create<DayStatisticsJob>().Build();
            dayStatisJob.JobDataMap.Add("commandDatas", commandDatas);
            var dayStatisTrigger = TriggerBuilder.Create()
                //.StartAt(DateTime.Now.GetToday().AddDays(1).AddMinutes(2))
                .StartAt(DateTime.Now.AddSeconds(30))
                .WithSimpleSchedule(x => x.WithIntervalInHours(1).RepeatForever())
                .Build();

            scheduler.ScheduleJob(dayStatisJob, dayStatisTrigger);
            
            var runStatisJob = JobBuilder.Create<DayStatisticsJob>().Build();
            runStatisJob.JobDataMap.Add("commandDatas", commandDatas);
            var runStatisTrigger = TriggerBuilder.Create()
                //.StartAt(DateTime.Now.GetToday().AddDays(1).AddMinutes(2))
                .StartAt(DateTime.Now.AddSeconds(30))
                .WithSimpleSchedule(x => x.WithIntervalInHours(1).RepeatForever())
                .Build();

            scheduler.ScheduleJob(runStatisJob, runStatisTrigger);
        }
    }
}

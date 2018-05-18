using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using JingAnWebService;
using Lampblack_Platform.Models.PlatfromAccess;
using Newtonsoft.Json;
using Platform.Cache;
using Platform.Process.Business;
using Quartz;
using SHWD.Platform.Repository.Entities;
using SHWDTech.Platform.Model.Business;
using SHWDTech.Platform.Utility;
using WebViewModels.Enums;

namespace Lampblack_Platform.Schedule
{
    //静安区环保局油烟数据接口，本接口主动向静安区平台发送油烟实时数据，每15分钟发送一次。
    public class JinganFifteenDataPostJob : IJob
    {
        private readonly JingAnLampblackService _service;

        private const string PlatformName = "JingAnLampblack";

        public JinganFifteenDataPostJob()
        {
            _service = new JingAnLampblackService();
        }

        public void Execute(IJobExecutionContext context)
        {
            var dataList = new List<JinganDeviceBaseInfo>();
            using (var ctx = new RepositoryDbContext())
            {
                var enabledDomains = ctx.UserDictionaries.Where(d => d.ItemName == PlatformName && d.ItemKey == "DataSource").ToList();
                foreach (var domain in enabledDomains)
                {
                    var domainId = domain.DomainId;
                    var regitedHotel = ctx.PlatformAccesses.Where(p => p.PlatformName == PlatformName && p.DomainId == domainId)
                    .Select(pt => pt.TargetGuid);
                    var devs = ctx.RestaurantDevices.Where(d => regitedHotel.Contains(d.ProjectId.Value))
                        .Include("Project")
                        .Include("LampblackDeviceModel")
                        .Select(d => new {
                            d.Id,
                            d.DeviceName,
                            d.Identity,
                            d.ProjectId,
                            d.DeviceCode,
                            d.DeviceNodeId,
                            ProjectIdentity = d.Project.Identity,
                            DeviceModelId = d.LampblackDeviceModel.Id
                        }).ToList();
                    var checkDate = DateTime.Now.AddMinutes(-15);
                    foreach (var dev in devs)
                    {
                        dataList.Clear();
                        var pData = ctx.ProtocolDatas.FirstOrDefault(p =>
                            p.DomainId == domainId && p.DeviceIdentity == dev.Identity && p.UpdateTime > checkDate);
                        var post = new JinganDeviceBaseInfo
                        {
                            ENTER_ID = dev.ProjectId.ToString().ToLower(),
                            DEVICE_NAME = dev.DeviceName,
                            DEVICE_CODE = dev.DeviceNodeId
                        };
                        if (pData != null)
                        {
                            var current = ctx.MonitorDatas.FirstOrDefault(d => d.DomainId == domainId
                                                                                       && d.ProjectIdentity == dev.ProjectIdentity
                                                                                       && d.DeviceIdentity == dev.Identity
                                                                                       && d.ProtocolDataId == pData.Id
                                                                                       && d.CommandDataId ==
                                                                                       CommandDataId.CleanerCurrent);

                            post.DEVICE_STATE = "1";
                            post.CLEAN_LINESS = $"{GetCleanRate(current?.DoubleValue, dev.DeviceModelId)}";
                            post.LAMPBLACK_VALUE = CalcDensity(current?.DoubleValue);
                            post.MONITORTIME = $"{current?.UpdateTime:yyyy-MM-dd HH:mm:ss}";
                        }
                        else
                        {
                            post.DEVICE_STATE = "0";
                            post.CLEAN_LINESS = "无数据";
                            post.LAMPBLACK_VALUE = "-1";
                            post.PURIFIER_STATE = "开";
                            post.FAN_STATE = "开";
                            post.MONITORTIME = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
                        }
                        dataList.Add(post);
                        var postJsonStr = JsonConvert.SerializeObject(dataList);
                        var response = _service.InsertDeviceBaseInfo(postJsonStr);
                        var msgs = JsonConvert.DeserializeObject<List<JinganApiResult>>(response);
                        if (!(msgs.Count > 0 && msgs[0].MESSAGE == "SUCCESS"))
                        {
                            LogService.Instance.Info(string.Join("\r\n", msgs.Select(m => m.MESSAGE)));
                        }
                    }
                }
            }
        }

        private string CalcDensity(double? current)
        {
            if (current == null || current < 0.001) return "0";
            return $"{Math.Round(4 - current.Value / 200, 3)}";
        }

        /// <summary>
        /// 获取清洁度值
        /// </summary>
        /// <param name="current"></param>
        /// <param name="modelId"></param>
        /// <returns></returns>
        private string GetCleanRate(double? current, Guid modelId)
        {
            var rater = (CleanessRate)PlatformCaches.GetCache($"CleanessRate-{modelId}").CacheItem;

            var rate = Lampblack.GetCleanessNumRate(current, rater);
            switch (rate)
            {
                case 1: return "合格";
                case 2: return "不合格";
            }

            return "无数据";
        }
    }
}
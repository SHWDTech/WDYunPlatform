﻿using System;
using System.Linq;
using System.Web.Http;
using Lampblack_Platform.Models;
using MvcWebComponents.Controllers;
using Platform.Process.Process;
using SHWD.Platform.Repository.Entities;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Utility;

namespace Lampblack_Platform.Controllers
{
    //黄浦区环保局油烟数据接口，本接口提供设备实时数据信息。
    public class IndexInfoController : WdApiControllerBase
    {
        public IndexInfo Get([FromUri]string domain)
        {
            var context = new RepositoryDbContext();
            var dics = context.SysDictionaries.Where(d => d.ItemName == "HuangpuPlatform").ToList();
            var domainId = Guid.Parse(dics.First(d => d.ItemKey == $"{domain.ToUpper()}DomainId").ItemValue);
            var area = ProcessInvoke<UserDictionaryProcess>().GetAreaByName("黄浦区");
            var model = new IndexInfo();
            var devsGroup = ProcessInvoke<RestaurantDeviceProcess>()
                .DevicesInDistrict(area.Id, device => device.DomainId == domainId && device.Status == DeviceStatus.Enabled)
                .OrderBy(d => d.Identity)
                .GroupBy(dev => dev.Hotel)
                .ToList();
            var checkDate = DateTime.Now.AddMinutes(-2);
            foreach (var group in devsGroup)
            {
                var ordered = group.OrderBy(d => d.Identity);
                foreach (var dev in ordered)
                {
                    var monitorDatas = ProcessInvoke<MonitorDataProcess>()
                        .GetDeviceCleanerCurrent(dev, checkDate, 0);
                    if (monitorDatas?.DoubleValue == null) continue;
                    var time = monitorDatas.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss");
                    var fan = new Index
                    {
                        EQUP_ID = $"{Convert.ToUInt32(Convert.ToUInt32(dev.DeviceNodeId, 16)):D4}1",
                        RMON_TIM = time,
                        EQUP_VAL = monitorDatas.DoubleValue > 0 ? "1" : "0"
                    };
                    model.data.Add(fan);
                    var cleaner = new Index
                    {
                        EQUP_ID = $"{Convert.ToUInt32(Convert.ToUInt32(dev.DeviceNodeId, 16)):D4}2",
                        RMON_TIM = time,
                        EQUP_VAL = monitorDatas.DoubleValue > 0 ? "1" : "0"
                    };
                    model.data.Add(cleaner);

                    var current = new Index
                    {
                        EQUP_ID = $"{Convert.ToUInt32(Convert.ToUInt32(dev.DeviceNodeId, 16)):D4}3",
                        RMON_TIM = time,
                        EQUP_VAL = $"{monitorDatas.DoubleValue.Value / 1000:F4}"
                    };
                    model.data.Add(current);
                }
            }

            model.result = "success";
            return model;
        }
    }
}

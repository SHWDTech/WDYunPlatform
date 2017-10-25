using System;
using Lampblack_Platform.Models;
using MvcWebComponents.Controllers;
using Platform.Process.Process;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Utility;

namespace Lampblack_Platform.Controllers
{
    public class IndexInfoController : WdApiControllerBase
    {
        public IndexInfo Get()
        {
            var area = ProcessInvoke<UserDictionaryProcess>().GetAreaByName("黄浦区");
            var model = new IndexInfo();
            var devs = ProcessInvoke<RestaurantDeviceProcess>()
                .DevicesInDistrict(area.Id, device => device.Status == DeviceStatus.Enabled);
            var checkDate = DateTime.Now.AddMinutes(-2);

            foreach (var dev in devs)
            {
                try
                {
                    foreach (var channel in dev.InUsingChannels)
                    {
                        var monitorDatas = ProcessInvoke<MonitorDataProcess>()
                            .GetDeviceCleanerCurrent(dev, checkDate, channel - 1);
                        if (monitorDatas?.DoubleValue == null) continue;
                        var time = monitorDatas.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss");
                        var fan = new Index
                        {
                            EQUP_ID = $"{Convert.ToUInt32(Convert.ToUInt32(dev.DeviceNodeId, 16)):D6}{channel}1",
                            RMON_TIM = time,
                            EQUP_VAL = monitorDatas.DoubleValue > 0 ? "1" : "0"
                        };
                        model.data.Add(fan);
                        var cleaner = new Index
                        {
                            EQUP_ID = $"{Convert.ToUInt32(Convert.ToUInt32(dev.DeviceNodeId, 16)):D6}{channel}2",
                            RMON_TIM = time,
                            EQUP_VAL = monitorDatas.DoubleValue > 0 ? "1" : "0"
                        };
                        model.data.Add(cleaner);

                        var current = new Index
                        {
                            EQUP_ID = $"{Convert.ToUInt32(Convert.ToUInt32(dev.DeviceNodeId, 16)):D6}{channel}3",
                            RMON_TIM = time,
                            EQUP_VAL = $"{monitorDatas.DoubleValue.Value/1000:F4}"
                        };
                        model.data.Add(current);
                    }
                }
                catch (Exception ex)
                {
                    LogService.Instance.Error("IndexInfo接口执行失败。", ex);
                    var currentException = ex;
                    while (currentException.InnerException != null)
                    {
                        LogService.Instance.Error("IndexInfo接口执行失败详细原因。", ex);
                        currentException = currentException.InnerException;
                    }

                    return model;
                }

            }

            model.result = "success";
            return model;
        }
    }
}

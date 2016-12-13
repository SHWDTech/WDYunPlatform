using System;
using Lampblack_Platform.Models;
using MvcWebComponents.Controllers;
using Platform.Process.Process;
using SHWDTech.Platform.Utility;

namespace Lampblack_Platform.Controllers
{
    public class IndexInfoController : WdApiControllerBase
    {
        public IndexInfo Get()
        {
            var model = new IndexInfo();
            var devs =
                ProcessInvoke<RestaurantDeviceProcess>()
                    .DevicesInDistrict(Guid.Parse("B20071A6-A30E-9FAD-4C7F-4C353641A645"));
            var checkDate = DateTime.Now.AddMinutes(-2);

            foreach (var device in devs)
            {
                try
                {
                    var monitorDatas = ProcessInvoke<MonitorDataProcess>()
                        .GetDeviceCleanerCurrent(device, checkDate);
                    if (monitorDatas?.DoubleValue == null) continue;
                    var time = monitorDatas.UpdateTime.ToString("yyyy-MM-dd HH:mm:ss");
                    var fan = new Index
                    {
                        EQUP_ID = $"{device.DeviceCode}001",
                        RMON_TIM = time,
                        EQUP_VAL = monitorDatas.DoubleValue.Value > 400 ? "0" : "1"
                    };
                    model.data.Add(fan);
                    var cleaner = new Index
                    {
                        EQUP_ID = $"{device.DeviceCode}002",
                        RMON_TIM = time,
                        EQUP_VAL = monitorDatas.DoubleValue.Value > 400 ? "0" : "1"
                    };
                    model.data.Add(cleaner);

                    var current = new Index
                    {
                        EQUP_ID = $"{device.DeviceCode}003",
                        RMON_TIM = time,
                        EQUP_VAL = monitorDatas.DoubleValue.Value.ToString("F4")
                    };
                    model.data.Add(current);
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
            return model;
        }
    }
}

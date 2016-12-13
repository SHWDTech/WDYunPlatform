using System;
using Lampblack_Platform.Models;
using MvcWebComponents.Controllers;
using Platform.Process.Process;
using SHWDTech.Platform.Utility;

namespace Lampblack_Platform.Controllers
{
    public class EqupInfoController : WdApiControllerBase
    {
        public EqupInfo Get()
        {
            try
            {
                var model = new EqupInfo();
                var devs =
                    ProcessInvoke<RestaurantDeviceProcess>()
                        .DevicesInDistrict(Guid.Parse("B20071A6-A30E-9FAD-4C7F-4C353641A645"));
                foreach (var device in devs)
                {
                    var equpFan = new Equp
                    {
                        EQUP_ID = $"{device.DeviceCode}001",
                        EQUP_NAM = "风机",
                        CASE_ID = $"HPLB{device.DeviceNodeId.Substring(4, 4)}",
                        EQUP_TYP = "风机",
                    };
                    model.data.Add(equpFan);

                    var equpCleaner = new Equp
                    {
                        EQUP_ID = $"{device.DeviceCode}002",
                        EQUP_NAM = "净化器",
                        CASE_ID = $"HPLB{device.DeviceNodeId.Substring(4, 4)}",
                        EQUP_TYP = "净化器",
                    };
                    model.data.Add(equpCleaner);

                    var equpRate = new Equp
                    {
                        EQUP_ID = $"{device.DeviceCode}003",
                        EQUP_NAM = "清洁度",
                        CASE_ID = $"HPLB{device.DeviceNodeId.Substring(4, 4)}",
                        EQUP_TYP = "清洁度",
                    };
                    model.data.Add(equpRate);
                }

                model.result = "success";
                return model;
            }
            catch (Exception ex)
            {
                LogService.Instance.Error("接口执行失败。",ex);
                var currentException = ex;
                while (currentException.InnerException != null)
                {
                    LogService.Instance.Error("接口执行失败详细原因。", ex);
                    currentException = currentException.InnerException;
                }

                return null;
            }
        }
    }
}

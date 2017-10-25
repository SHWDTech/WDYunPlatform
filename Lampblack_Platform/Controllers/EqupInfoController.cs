using System;
using System.Linq;
using Lampblack_Platform.Models;
using MvcWebComponents.Controllers;
using Platform.Process.Process;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Utility;

namespace Lampblack_Platform.Controllers
{
    public class EqupInfoController : WdApiControllerBase
    {
        public EqupInfo Get()
        {
            try
            {
                var area = ProcessInvoke<UserDictionaryProcess>().GetAreaByName("黄浦区");
                var model = new EqupInfo();
                var devs = ProcessInvoke<RestaurantDeviceProcess>()
                    .DevicesInDistrict(area.Id, device => device.Status == DeviceStatus.Enabled)
                    .OrderBy(d=> d.Identity).ToList();
                foreach (var dev in devs)
                {
                    var alpha = 65;
                    foreach (var channel in dev.InUsingChannels)
                    {
                        var equpFan = new Equp
                        {
                            EQUP_ID = $"{Convert.ToUInt32(dev.DeviceNodeId, 16):D6}{channel}1",
                            EQUP_NAM = $"风机{(char)alpha}",
                            CASE_ID = $"QDHP{dev.Identity:D6}",
                            EQUP_TYP = "风机",
                            EQUP_MOD = "",
                            EQUP_LIM = ""
                        };
                        model.data.Add(equpFan);

                        var equpCleaner = new Equp
                        {
                            EQUP_ID = $"{Convert.ToUInt32(dev.DeviceNodeId, 16):D6}{channel}2",
                            EQUP_NAM = $"净化器{(char)alpha}",
                            CASE_ID = $"QDHP{dev.Identity:D6}",
                            EQUP_TYP = "净化器",
                            EQUP_MOD = "",
                            EQUP_LIM = ""
                        };
                        model.data.Add(equpCleaner);

                        var equpRate = new Equp
                        {
                            EQUP_ID = $"{Convert.ToUInt32(dev.DeviceNodeId, 16):D6}{channel}3",
                            EQUP_NAM = $"清洁度{(char)alpha}",
                            CASE_ID = $"QDHP{dev.Identity:D6}",
                            EQUP_TYP = "清洁度",
                            EQUP_MOD = "0.001",
                            EQUP_LIM = "0.6||0.1"
                        };
                        model.data.Add(equpRate);
                        alpha++;
                    }
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

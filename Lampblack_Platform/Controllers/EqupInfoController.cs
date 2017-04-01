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
                var model = new EqupInfo();
                var hotels =
                    ProcessInvoke<HotelRestaurantProcess>()
                        .HotelsInDistrict(Guid.Parse("B20071A6-A30E-9FAD-4C7F-4C353641A645"));
                foreach (var hotel in hotels)
                {
                    var devs = ProcessInvoke<RestaurantDeviceProcess>().GetDevicesByRestaurant(hotel.Id);
                    if (devs.Count(d => d.Status == DeviceStatus.Enabled) <= 0) continue;
                    var device = devs.OrderBy(d => d.Identity).First();
                    var equpFan = new Equp
                    {
                        EQUP_ID = $"{Convert.ToUInt32(device.DeviceNodeId, 16):D6}_01001",
                        EQUP_NAM = "风机",
                        CASE_ID = $"QDHP{device.Project.Identity:D6}",
                        EQUP_TYP = "风机",
                        EQUP_MOD = "",
                        EQUP_LIM = ""
                    };
                    model.data.Add(equpFan);

                    var equpCleaner = new Equp
                    {
                        EQUP_ID = $"{Convert.ToUInt32(device.DeviceNodeId, 16):D6}_01002",
                        EQUP_NAM = "净化器",
                        CASE_ID = $"QDHP{device.Project.Identity:D6}",
                        EQUP_TYP = "净化器",
                        EQUP_MOD = "",
                        EQUP_LIM = ""
                    };
                    model.data.Add(equpCleaner);

                    var equpRate = new Equp
                    {
                        EQUP_ID = $"{Convert.ToUInt32(device.DeviceNodeId, 16):D6}_01003",
                        EQUP_NAM = "清洁度",
                        CASE_ID = $"QDHP{device.Project.Identity:D6}",
                        EQUP_TYP = "清洁度",
                        EQUP_MOD = "0.001",
                        EQUP_LIM = "0.6||0.1"
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

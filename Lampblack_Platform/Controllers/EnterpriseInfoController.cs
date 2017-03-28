using System;
using System.Linq;
using Lampblack_Platform.Models;
using MvcWebComponents.Controllers;
using Platform.Process.Process;

namespace Lampblack_Platform.Controllers
{
    public class EnterpriseInfoController : WdApiControllerBase
    {
        public EnterpriseInfo Get()
        {
            var model = new EnterpriseInfo();
            var devs =
                    ProcessInvoke<RestaurantDeviceProcess>()
                        .DevicesInDistrict(Guid.Parse("B20071A6-A30E-9FAD-4C7F-4C353641A645"));
            foreach (var dev in devs)
            {
                var enterp = new Enterprise
                {
                    QYBM = dev.Hotel.ProjectCode,
                    QYMC = $"{dev.Hotel.RaletedCompany.CompanyName}({dev.Hotel.ProjectName})",
                    QYDZ = dev.Hotel.AddressDetail,
                    PER = dev.Hotel.ChargeMan,
                    TEL = dev.Hotel.Telephone,
                    QYSTREET = dev.Hotel.Street.ItemValue,
                    XPOS = dev.Hotel.Longitude.ToString(),
                    YPOS = dev.Hotel.Latitude.ToString(),
                    CASE_ID = $"QDHP{Convert.ToUInt32(Convert.ToUInt32(dev.DeviceNodeId, 16)):D6}",
                    CASE_NAM = dev.DeviceName,
                };

                model.data.Add(enterp);
            }

            model.result = "success";

            return model;
        }
    }
}

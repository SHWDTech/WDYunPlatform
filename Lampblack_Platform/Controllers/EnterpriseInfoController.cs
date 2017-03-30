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
            var hotels =
                    ProcessInvoke<HotelRestaurantProcess>()
                        .HotelsInDistrict(Guid.Parse("B20071A6-A30E-9FAD-4C7F-4C353641A645"));
            foreach (var hotel in hotels)
            {
                var devs = ProcessInvoke<RestaurantDeviceProcess>().GetDevicesByRestaurant(hotel.Id);
                if (devs.Count <= 0) continue;
                var enterp = new Enterprise
                {
                    QYBM = hotel.ProjectCode,
                    QYMC = $"{hotel.RaletedCompany.CompanyName}({hotel.ProjectName})",
                    QYDZ = hotel.AddressDetail,
                    PER = hotel.ChargeMan,
                    TEL = hotel.Telephone,
                    QYSTREET = hotel.Street.ItemValue,
                    XPOS = hotel.Longitude.ToString(),
                    YPOS = hotel.Latitude.ToString()
                };
                var dev = devs.OrderBy(d => d.Identity).First();
                enterp.CASE_ID = $"QDHP{hotel.Identity:D6}";
                enterp.CASE_NAM = dev.DeviceName;

                model.data.Add(enterp);
            }

            model.result = "success";

            return model;
        }
    }
}

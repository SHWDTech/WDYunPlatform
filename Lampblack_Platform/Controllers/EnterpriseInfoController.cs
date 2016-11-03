using System;
using System.Linq;
using Lampblack_Platform.Models;
using MvcWebComponents.Controllers;
using Platform.Process;
using Platform.Process.Process;

namespace Lampblack_Platform.Controllers
{
    public class EnterpriseInfoController : WdApiControllerBase
    {
        public EnterpriseInfo Get()
        {
            var model = new EnterpriseInfo();
            var hotels = ProcessInvoke.GetInstance<HotelRestaurantProcess>().HotelsInDistrict(Guid.Parse("24018BA6-481E-CFD3-5561-F3C2634397C4"));
            foreach (var hotel in hotels)
            {
                var enterp = new Enterprise()
                {
                    QYBM = hotel.ProjectCode,
                    QYMC = hotel.ProjectName,
                    QYDZ = hotel.AddressDetail,
                    QYSTREET = hotel.Street.ItemValue,
                    XPOS = hotel.Longitude.ToString(),
                    YPOS = hotel.Latitude.ToString(),
                };

                var devs = ProcessInvoke.GetInstance<RestaurantDeviceProcess>().GetDevicesByRestaurant(hotel.Id);
                if (devs.Count > 0)
                {
                    enterp.CASE_ID = devs.First().Id.ToString();
                    enterp.CASE_NAM = devs.First().DeviceName;
                }

                model.data.Add(enterp);
            }

            model.result = "success";

            return model;
        }
    }
}

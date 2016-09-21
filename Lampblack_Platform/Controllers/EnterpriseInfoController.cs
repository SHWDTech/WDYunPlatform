using System;
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
            var hotels = ProcessInvoke.GetInstance<HotelRestaurantProcess>().HotelsInDistrict(Guid.Parse("B20071A6-A30E-9FAD-4C7F-4C353641A645"));
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

                model.data.Add(enterp);
            }

            model.result = "success";

            return model;
        }
    }
}

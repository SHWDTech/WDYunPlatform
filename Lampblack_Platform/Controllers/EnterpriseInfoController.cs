using System.Linq;
using Lampblack_Platform.Models;
using MvcWebComponents.Controllers;
using Platform.Process.Process;
using SHWDTech.Platform.Model.Enums;

namespace Lampblack_Platform.Controllers
{
    public class EnterpriseInfoController : WdApiControllerBase
    {
        public EnterpriseInfo Get()
        {
            var area = ProcessInvoke<UserDictionaryProcess>().GetAreaByName("黄浦区");
            var model = new EnterpriseInfo();
            var devsGroup = ProcessInvoke<RestaurantDeviceProcess>()
                .DevicesInDistrict(area.Id, device => device.Status == DeviceStatus.Enabled)
                .OrderBy(d => d.Identity)
                .GroupBy(dev => dev.Hotel);
            foreach (var group in devsGroup)
            {
                var ordered = group.OrderBy(d => d.Identity);
                var alpha = 65;
                int? endfix = null;
                if (group.Count() > 1) endfix = 1;
                foreach (var dev in ordered)
                {
                    var enterp = new Enterprise
                    {
                        QYBM = $"QDHB{dev.Hotel.Identity:D4}{endfix:D2}",
                        QYMC = $"{dev.Hotel.RaletedCompany.CompanyName}({dev.Hotel.ProjectName})",
                        QYDZ = dev.Hotel.AddressDetail,
                        PER = dev.Hotel.ChargeMan,
                        TEL = dev.Hotel.Telephone,
                        QYSTREET = dev.Hotel.Street.ItemValue,
                        XPOS = dev.Hotel.Longitude.ToString(),
                        YPOS = dev.Hotel.Latitude.ToString(),
                        CASE_ID = $"QDHB{dev.Identity:D6}",
                        CASE_NAM = $"{(char)alpha}设备箱"
                    };

                    model.data.Add(enterp);
                    alpha++;
                    if (endfix != null)
                    {
                        endfix++;
                    }
                }
            }

            model.result = "success";

            return model;
        }
    }
}

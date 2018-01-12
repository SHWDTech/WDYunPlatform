using System;
using System.Linq;
using System.Web.Http;
using Lampblack_Platform.Models;
using MvcWebComponents.Controllers;
using Platform.Process.Process;
using SHWD.Platform.Repository.Entities;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Utility;

namespace Lampblack_Platform.Controllers
{
    //黄浦区环保局油烟数据接口，本接口提供酒店相关信息。
    public class EnterpriseInfoController : WdApiControllerBase
    {
        public EnterpriseInfo Get([FromUri]string domain)
        {
            try
            {
                var context = new RepositoryDbContext();
                var dics = context.SysDictionaries.Where(d => d.ItemName == "HuangpuPlatform").ToList();
                var domainId = Guid.Parse(dics.First(d => d.ItemKey == $"{domain.ToUpper()}DomainId").ItemValue);
                var prefix = dics.First(d => d.ItemKey == $"{domain.ToUpper()}Prefix").ItemValue;
                var area = ProcessInvoke<UserDictionaryProcess>().GetAreaByName("黄浦区");
                var model = new EnterpriseInfo();
                var devsGroup = ProcessInvoke<RestaurantDeviceProcess>()
                    .DevicesInDistrict(area.Id, device => device.DomainId == domainId && device.Status == DeviceStatus.Enabled)
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
                            QYBM = $"{prefix}{dev.Hotel.Identity:D4}{endfix:D2}",
                            QYMC = $"{dev.Hotel.RaletedCompany.CompanyName}({dev.Hotel.ProjectName})",
                            QYDZ = dev.Hotel.AddressDetail,
                            PER = dev.Hotel.ChargeMan,
                            TEL = dev.Hotel.Telephone,
                            QYSTREET = dev.Hotel.Street.ItemValue,
                            XPOS = dev.Hotel.Longitude.ToString(),
                            YPOS = dev.Hotel.Latitude.ToString(),
                            CASE_ID = $"{prefix}{dev.Identity:D6}",
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
            catch (Exception ex)
            {
                LogService.Instance.Error("EnterpriseInfoError", ex);
                throw ex;
            }
        }
    }
}

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
    //黄浦区环保局油烟数据接口，本接口提供设备相关信息。
    public class EqupInfoController : WdApiControllerBase
    {
        public EqupInfo Get([FromUri]string domain)
        {
            try
            {
                var context = new RepositoryDbContext();
                var dics = context.SysDictionaries.Where(d => d.ItemName == "HuangpuPlatform").ToList();
                var domainId = Guid.Parse(dics.First(d => d.ItemKey == $"{domain.ToUpper()}DomainId").ItemValue);
                var prefix = dics.First(d => d.ItemKey == $"{domain.ToUpper()}Prefix").ItemValue;
                var area = ProcessInvoke<UserDictionaryProcess>().GetAreaByName("黄浦区");
                var model = new EqupInfo();
                var devsGroup = ProcessInvoke<RestaurantDeviceProcess>()
                    .DevicesInDistrict(area.Id, device => device.DomainId == domainId && device.Status == DeviceStatus.Enabled)
                    .OrderBy(d=> d.Identity)
                    .GroupBy(dev => dev.Hotel);
                foreach (var group in devsGroup)
                {
                    var ordered = group.OrderBy(d => d.Identity);
                    var alpha = 65;
                    foreach (var dev in ordered)
                    {
                        var equpFan = new Equp
                        {
                            EQUP_ID = $"{Convert.ToUInt32(dev.DeviceNodeId, 16):D6}1",
                            EQUP_NAM = $"风机{(char)alpha}",
                            CASE_ID = $"{prefix}{dev.Identity:D6}",
                            EQUP_TYP = "风机",
                            EQUP_MOD = "",
                            EQUP_LIM = ""
                        };
                        model.data.Add(equpFan);

                        var equpCleaner = new Equp
                        {
                            EQUP_ID = $"{Convert.ToUInt32(dev.DeviceNodeId, 16):D6}2",
                            EQUP_NAM = $"净化器{(char)alpha}",
                            CASE_ID = $"{prefix}{dev.Identity:D6}",
                            EQUP_TYP = "净化器",
                            EQUP_MOD = "",
                            EQUP_LIM = ""
                        };
                        model.data.Add(equpCleaner);

                        var equpRate = new Equp
                        {
                            EQUP_ID = $"{Convert.ToUInt32(dev.DeviceNodeId, 16):D6}3",
                            EQUP_NAM = $"清洁度{(char)alpha}",
                            CASE_ID = $"{prefix}{dev.Identity:D6}",
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

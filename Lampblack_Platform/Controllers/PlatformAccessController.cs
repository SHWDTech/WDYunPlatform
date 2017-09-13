using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Lampblack_Platform.Models.BootstrapTable;
using MvcWebComponents.Attributes;
using MvcWebComponents.Controllers;
using Platform.Process.Process;
using WebViewModels.ViewDataModel;
using JingAnWebService;
using Lampblack_Platform.Models.PlatfromAccess;
using Newtonsoft.Json;

namespace Lampblack_Platform.Controllers
{
    public class PlatformAccessController : WdControllerBase
    {
        private const string PlatformName = "JingAnLampblack";

        // GET: PlatformAccess
        public ActionResult JingAnPlatform()
        {
            return View();
        }

        [NamedAuth(Modules = nameof(JingAnPlatform))]
        public ActionResult JinganAccessTable(JinganPlatfromDevice post)
        {
            var dis = ProcessInvoke<UserDictionaryProcess>().GetAreaByName("静安区");
            var regedDevs = ProcessInvoke<PlatformAccessProcess>()
                .GetPlatformAccessesByPlatformName(PlatformName).Select(p => p.TargetGuid).ToList();
            var hotels = ProcessInvoke<HotelRestaurantProcess>()
                .GetHotelRestaurantByArea(dis.Id, Guid.Empty, Guid.Empty);
            if (!string.IsNullOrWhiteSpace(post.QueryName))
            {
                hotels = hotels.Where(d => d.ProjectName.Contains(post.QueryName));
            }
            var total = hotels.Count();
            var rows = hotels.OrderBy(h => h.Id).Skip(post.offset).Take(post.limit)
                .ToList()
                .Select(d => new JinganPlatformInfo
                {
                    Id = d.Id,
                    AccessStatus = regedDevs.Contains(d.Id) ? "已注册" : "未注册",
                    DistrictName = d.District.ItemValue,
                    ProjectName = d.ProjectName,
                    Address = d.AddressDetail,
                    Longitude = $"{d.Longitude}",
                    Latitude = $"{d.Latitude}"
                });

            return JsonTable(new
            {
                total,
                rows
            });
        }

        [NamedAuth(Modules = nameof(JingAnPlatform))]
        public ActionResult JinganRegister(Guid id)
        {
            var hotel = ProcessInvoke<HotelRestaurantProcess>().GetHotelById(id);
            if (hotel == null) return Json("未找到指定酒店！", JsonRequestBehavior.AllowGet);
            var service = new JingAnLampblackService();
            var postList = new List<JinganEnterBaseInfo>
            {
                new JinganEnterBaseInfo
                {
                    ENTER_ID = hotel.Id.ToString().ToLower(),
                    ENTER_NAME = hotel.ProjectName,
                    ADDRESS = hotel.AddressDetail,
                    LONGITUDE = $"{hotel.Longitude}",
                    LATITUDE = $"{hotel.Latitude}"
                }
            };
            var response = service.InsertBaseInfo(JsonConvert.SerializeObject(postList));
            var msgs = JsonConvert.DeserializeObject<List<JinganApiResult>>(response);
            if (msgs.Count > 0 && msgs[0].MESSAGE == "SUCCESS")
            {
                ProcessInvoke<PlatformAccessProcess>().AddNoewPlatformAccessRegister(PlatformName, id);
                return Json("注册成功！", JsonRequestBehavior.AllowGet);
            }
            return Json($"注册失败，错误原因：\r\n{string.Join("\r\n", msgs.Select(m => m.MESSAGE))}", JsonRequestBehavior.AllowGet);
        }
    }
}
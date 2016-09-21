using System;
using Lampblack_Platform.Models;
using MvcWebComponents.Controllers;
using Platform.Process;
using Platform.Process.Enums;
using Platform.Process.Process;

namespace Lampblack_Platform.Controllers
{
    public class MonitorInfoListController : WdApiControllerBase
    {
        public MonitorInfos Get()
        {
            var model = new MonitorInfos();

            var hotels = ProcessInvoke.GetInstance<HotelRestaurantProcess>().HotelsInDistrict(Guid.Parse("B20071A6-2015-B0B2-1902-F6D82F45B845"));
            foreach (var hotel in hotels)
            {
                var status = ProcessInvoke.GetInstance<HotelRestaurantProcess>().GetHotelCurrentStatus(hotel.Id);
                var data = new MonitorInfo()
                {
                    entp_id = hotel.ProjectCode,
                    entp_nam = hotel.ProjectName,
                    entp_ndr = Math.Round((double)status["LampblackIn"], 3),
                    entp_ndc = Math.Round((double)status["LampblackOut"], 3),
                    entp_qjl = TransferRate(status["CleanRate"].ToString()),
                    entp_jhqkg = (bool)status["CleanerStatus"] ? 1 : 0,
                    entp_fjkg = (bool)status["FanStatus"] ? 1 : 0,
                    entp_adr = hotel.AddressDetail
                };
                if (status.ContainsKey("UpdateTime"))
                {
                    data.entp_jcsj = ((DateTime) status["UpdateTime"]).ToString("yyyy-MM-dd HH:mm:ss");
                }

                model.data.Add(data);
            }

            model.result = "success";

            return model;
        }

        private int TransferRate(string rate)
        {
            if (rate == CleanessRateResult.NoData)
            {
                return -1;
            }
            if (rate == CleanessRateResult.Fail || rate == CleanessRateResult.Worse)
            {
                return 2;
            }
            if (rate == CleanessRateResult.Qualified)
            {
                return 1;
            }

            return 0;
        }
    }
}

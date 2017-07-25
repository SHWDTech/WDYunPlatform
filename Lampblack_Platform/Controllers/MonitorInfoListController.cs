using System;
using Lampblack_Platform.Models;
using MvcWebComponents.Controllers;
using Platform.Process.Enums;
using Platform.Process.Process;

namespace Lampblack_Platform.Controllers
{
    public class MonitorInfoListController : WdApiControllerBase
    {
        public MonitorInfos Get()
        {
            var model = new MonitorInfos();

            var processer = ProcessInvoke<HotelRestaurantProcess>();
            var hotels = processer.HotelsInDistrict(Guid.Parse("B20071A6-2015-B0B2-1902-F6D82F45B845"));
            foreach (var hotel in hotels)
            {
                var status = processer.GetHotelCurrentStatus(hotel.Id);
                var data = new MonitorInfo
                {
                    entp_id = hotel.ProjectCode,
                    entp_nam = hotel.ProjectName,
                    entp_ndr = Math.Round((double)status.LampblackIn, 3),
                    entp_ndc = Math.Round((double)status.LampblackOut, 3),
                    entp_qjl = TransferRate(status.CleanRate),
                    entp_jhqkg = status.CleanerSwitch ? 1 : 0,
                    entp_fjkg = status.FanSwitch ? 1 : 0,
                    entp_adr = hotel.AddressDetail
                };
                var updateTime = new DateTime(status.UpdateTime);
                if (updateTime > DateTime.MinValue)
                {
                    data.entp_jcsj = $"{updateTime:yyyy-MM-dd HH:mm:ss}";
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

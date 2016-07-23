using System;
using SHWDTech.Platform.Utility.ExtensionMethod;

namespace Platform.Process.Process
{
    public class DataStatisticsProcess : ProcessBase
    {
        public void ProduceHourStatis()
        {
            var hotels = ProcessInvoke.GetInstance<HotelRestaurantProcess>().GetAllHotelGuids();

            var start = DateTime.Now.GetCurrentHour();

            var end = DateTime.Now.GetNextHour();
            foreach (var hotel in hotels)
            {
                
            }
        }
    }
}

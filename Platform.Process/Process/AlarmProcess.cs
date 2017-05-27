using System.Collections.Generic;
using System.Linq;
using SHWD.Platform.Repository.Repository;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Utility.ExtensionMethod;
using WebViewModels.ViewDataModel;

namespace Platform.Process.Process
{
    public class AlarmProcess : ProcessBase
    {
        public List<AlarmTableView> GetAlarmTableData(int offset, int limit, out int total)
        {
            total = Repo<AlarmRepository>().GetCount(null);
            var rows = Repo<AlarmRepository>().GetAllModels().OrderBy(al => al.UpdateTime).Skip(offset).Take(limit)
                .ToList()
                .Select(a => new AlarmTableView
                {
                    DistrictName = GetDistrictName(a.AlarmDevice.Hotel.DistrictId),
                    HotelName = a.AlarmDevice.Project.ProjectName,
                    DeviceName = a.AlarmDevice.DeviceCode,
                    AlarmType = EnumHelper<AlarmType>.GetDisplayValue(a.AlarmType)
                }).ToList();

            return rows;
        }
    }
}

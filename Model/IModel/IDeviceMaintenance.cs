using System;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.Model;

namespace SHWDTech.Platform.Model.IModel
{
    public interface IDeviceMaintenance : ISysDomainModel
    {
        Guid MaintenanceUserId { get; set; }

        WdUser MaintenanceUser { get; set; }

        Guid DeviceId { get; set; }

        Device Device { get; set; }

        DateTime MaintenanceDateTime { get; set; }

        DeviceMaintenanceStatus BeforeMaintenance { get; set; }

        DeviceMaintenanceStatus AfterMaintenance { get; set; }

        string Comment { get; set; }
    }
}

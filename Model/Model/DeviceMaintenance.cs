using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SHWDTech.Platform.Model.Enums;
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;

namespace SHWDTech.Platform.Model.Model
{
    public class DeviceMaintenance : SysDomainModelBase, IDeviceMaintenance
    {
        [Display(Name = "维护人")]
        public virtual Guid MaintenanceUserId { get; set; }

        [ForeignKey("MaintenanceUserId")]
        public virtual WdUser MaintenanceUser { get; set; }

        [Display(Name = "被维护设备")]
        public virtual Guid DeviceId { get; set; }

        [ForeignKey("DeviceId")]
        public virtual Device Device { get; set; }

        [Display(Name = "维护时间")]
        [DataType(DataType.DateTime)]
        public virtual DateTime MaintenanceDateTime { get; set; }

        [Display(Name = "维护前状态")]
        public virtual DeviceMaintenanceStatus BeforeMaintenance { get; set; }

        [Display(Name = "维护后状态")]
        public virtual DeviceMaintenanceStatus AfterMaintenance { get; set; }

        [Display(Name = "备注")]
        [MaxLength(2000)]
        public virtual string Comment { get; set; }
    }
}

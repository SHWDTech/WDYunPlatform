using System;
using System.ComponentModel.DataAnnotations;
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;

namespace SHWDTech.Platform.Model.Model
{
    [Serializable]
    public class Device :SysModelBase, IDevice
    {
        [Key]
        public int DeviceId { get; set; }

        [Display(Name = "设备所属域")]
        public SysDomain DeviceDomain { get; set; }

        [Display(Name = "设备编码")]
        [MaxLength(25)]
        public string DeviceCode { get; set; }

        [Display(Name = "设备名称")]
        [MaxLength(25)]
        public string DeviceName { get; set; }

        [Display(Name = "设备NODE编码")]
        [MaxLength(256)]
        public byte[] DeviceNodeId { get; set; }

        [Display(Name = "设备所属项目")]
        public Project Project { get; set; }

        [Display(Name = "设备启用时间")]
        public DateTime StartTime { get; set; }

        [Display(Name = "设备预定启用时间")]
        public DateTime PreEndTime { get; set; }

        [Display(Name = "设备结束时间")]
        public DateTime EndTime { get; set; }

        [Display(Name = "设备是否启用")]
        public bool IsEnabled { get; set; }

        [Display(Name = "设备状态")]
        public int Status { get; set; }

        [Display(Name = "设备关联摄像头")]
        public Camera Camera { get; set; }
    }
}

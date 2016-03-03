using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;
using System;
using System.ComponentModel.DataAnnotations;

namespace SHWDTech.Platform.Model.Model
{
    /// <summary>
    /// 设备模型
    /// </summary>
    [Serializable]
    public class Device : SysDomainModelBase, IDevice
    {
        [Required]
        [Display(Name = "设备类型")]
        [MaxLength(50)]
        public DeviceType DeviceType { get; set; }

        [Display(Name = "设备对应的原设备")]
        public Device OriginalDevice { get; set; }

        [Required]
        [Display(Name = "设备名称")]
        [MaxLength(25)]
        public string DeviceName { get; set; }

        [MaxLength(25)]
        [Display(Name = "设备访问密码")]
        public string DevicePassword { get; set; }

        [Required]
        [Display(Name = "设备唯一标识符")]
        public Guid DeviceGuid { get; set; }

        [Display(Name = "设备NODE编码")]
        [MaxLength(16)]
        public byte[] DeviceNodeId { get; set; }

        [Required]
        [Display(Name = "设备关联固件集")]
        public FirmwareSet FirmwareSet { get; set; }

        [Display(Name = "设备所属项目")]
        public Project Project { get; set; }

        [Display(Name = "设备启用时间")]
        public DateTime StartTime { get; set; }

        [Display(Name = "设备预定启用时间")]
        public DateTime PreEndTime { get; set; }

        [Display(Name = "设备结束时间")]
        public DateTime EndTime { get; set; }

        [Display(Name = "设备状态")]
        public int Status { get; set; }

        [Display(Name = "设备关联摄像头")]
        public Camera Camera { get; set; }
    }
}
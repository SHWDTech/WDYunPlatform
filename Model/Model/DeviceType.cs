using System;
using System.ComponentModel.DataAnnotations;
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;

namespace SHWDTech.Platform.Model.Model
{
    /// <summary>
    /// 设备类型
    /// </summary>
    [Serializable]
    public class DeviceType : SysModelBase, IDeviceType
    {
        [Required]
        [Display(Name = "设备应用领域")]
        public SysDictionary Field { get; set; }

        [Required]
        [Display(Name = "设备应用子领域")]
        public SysDictionary SubField { get; set; }

        [Required]
        [Display(Name = "设备自定义字段")]
        public string CustomerInfo { get; set; }

        [Required]
        [Display(Name = "设备版本")]
        public string Version { get; set; }

        [Required]
        [Display(Name = "设备发布时间")]
        public DateTime ReleaseDateTime { get; set; }

        [Required]
        [Display(Name = "设备类型编码")]
        public string DeviceTypeCode { get; set; }
    }
}

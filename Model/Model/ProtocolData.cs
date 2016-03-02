using System;
using System.ComponentModel.DataAnnotations;
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;

namespace SHWDTech.Platform.Model.Model
{
    /// <summary>
    /// 协议数据
    /// </summary>
    [Serializable]
    public class ProtocolData : DataModelBase, IProtocolData
    {
        [Required]
        [Display(Name = "协议所属设备")]
        public Device Device { get; set; }

        [Required]
        [Display(Name = "协议内容")]
        public byte[] ProtocolContent { get; set; }

        [Required]
        [Display(Name = "协议长度")]
        public int Length { get; set; }

        [Required]
        [Display(Name = "协议类型")]
        public int ProtocolType { get; set; }

        [Required]
        [Display(Name = "协议版本")]
        public int ProtocolVersion { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime UpdateTime { get; set; }
    }
}

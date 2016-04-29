using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SHWDTech.Platform.Model.Model
{
    /// <summary>
    /// 协议数据
    /// </summary>
    [Serializable]
    public class ProtocolData : DataModelBase, IProtocolData
    {
        [Required]
        [Display(Name = "协议所属设备ID")]
        public virtual Guid DeviceId { get; set; }

        [Display(Name = "协议所属设备")]
        [ForeignKey("DeviceId")]
        public virtual Device Device { get; set; }

        [Required]
        [Display(Name = "协议内容")]
        public virtual byte[] ProtocolContent { get; set; }

        [Required]
        [Display(Name = "协议长度")]
        public virtual int Length { get; set; }

        [Required]
        [Display(Name = "协议类型ID")]
        public virtual Guid ProtocolId { get; set; }

        [Display(Name = "协议类型")]
        [ForeignKey("ProtocolId")]
        public virtual Protocol Protocol { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "协议包组包完成时间")]
        public virtual DateTime ProtocolTime { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "协议数据更新时间")]
        public virtual DateTime UpdateTime { get; set; }
    }
}
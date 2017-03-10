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
        [Index("Ix_Device_UpdateTime", IsClustered = true, Order = 0)]
        public virtual long DeviceIdentity { get; set; }

        [Required]
        [Display(Name = "协议内容")]
        public virtual byte[] ProtocolContent { get; set; }

        [Required]
        [Display(Name = "协议长度")]
        public virtual int Length { get; set; }

        [Required]
        [Display(Name = "协议类型ID")]
        public virtual Guid ProtocolId { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "协议包组包完成时间")]
        public virtual DateTime ProtocolTime { get; set; }

        [DataType(DataType.DateTime)]
        [Index("Ix_Device_UpdateTime", IsClustered = true, Order = 1)]
        [Index("Ix_UpdateTime")]
        [Display(Name = "协议数据更新时间")]
        public virtual DateTime UpdateTime { get; set; }
    }
}
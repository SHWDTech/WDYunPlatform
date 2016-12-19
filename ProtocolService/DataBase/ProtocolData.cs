using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SHWDTech.Platform.StorageConstrains.Model;

namespace SHWDTech.Platform.ProtocolService.DataBase
{
    [Serializable]
    public class ProtocolData : LongModel, IProtocolData
    {
        [Required]
        [MaxLength(500)]
        public virtual string Business { get; set; }

        [Required]
        [MaxLength(500)]
        public virtual string DeviceNodeId { get; set; }

        [Required]
        [Display(Name = "协议内容")]
        public virtual byte[] ProtocolContent { get; set; }

        [Required]
        [Display(Name = "协议字符串显示")]
        public virtual string ProtocolString { get; set; }

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
        public virtual DateTime PackageDateTime { get; set; }

        [DataType(DataType.DateTime)]
        [Index("Ix_Device_UpdateTime", Order = 1)]
        [Index("Ix_UpdateTime")]
        [Display(Name = "协议数据更新时间")]
        public virtual DateTime UpdateTime { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SHWDTech.Platform.StorageConstrains.Model;

namespace SHWDTech.Platform.ProtocolService.DataBase
{
    /// <summary>
    /// 协议结构模型类
    /// </summary>
    [Serializable]
    public class ProtocolStructure : GuidModel, IProtocolStructure
    {
        [Required]
        [Display(Name = "所属协议ID")]
        public virtual Guid ProtocolId { get; set; }

        [Display(Name = "所属协议")]
        [ForeignKey("ProtocolId")]
        public virtual Protocol Protocol { get; set; }

        [Required]
        [Display(Name = "协议段数据类型")]
        public virtual string DataType { get; set; }

        [Required]
        [Display(Name = "协议段名称")]
        [MaxLength(50)]
        public virtual string StructureName { get; set; }

        [Required]
        [Display(Name = "协议段索引值")]
        public virtual int StructureIndex { get; set; }

        [Required]
        [Display(Name = "协议段数据长度")]
        public virtual int StructureDataLength { get; set; }

        [Required]
        [Display(Name = "协议段默认值")]
        public byte[] DefaultBytes { get; set; }
    }
}

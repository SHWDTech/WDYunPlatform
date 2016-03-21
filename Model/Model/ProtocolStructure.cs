using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;

namespace SHWDTech.Platform.Model.Model
{
    /// <summary>
    /// 协议接口模型类
    /// </summary>
    [Serializable]
    public class ProtocolStructure : SysModelBase, IProtocolStructure
    {
        [Required]
        [Display(Name = "所属协议ID")]
        public virtual Guid ProtocolId { get; set; }

        [Display(Name = "所属协议")]
        [ForeignKey("ProtocolId")]
        public virtual Protocol Procotol { get; set; }

        [Required]
        [Display(Name = "协议段数据类型")]
        public virtual string DataType { get; set; }

        [Required]
        [Display(Name = "协议段名称")]
        [MaxLength(50)]
        public virtual string ComponentName { get; set; }

        [Required]
        [Display(Name = "协议段索引值")]
        public virtual int ComponentIndex { get; set; }

        [Required]
        [Display(Name = "协议段数据长度")]
        public virtual int ComponentDataLength { get; set; }
    }
}
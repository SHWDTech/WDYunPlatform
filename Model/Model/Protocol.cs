using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SHWDTech.Platform.Model.Model
{
    /// <summary>
    /// 协议
    /// </summary>
    [Serializable]
    public class Protocol : SysModelBase, IProtocol
    {
        [Required]
        [Display(Name = "协议应用领域ID")]
        public virtual Guid FieldId { get; set; }

        [Display(Name = "协议应用领域")]
        [ForeignKey("FieldId")]
        public virtual SysDictionary Field { get; set; }

        [Required]
        [Display(Name = "协议应用子领域")]
        public virtual Guid SubFieldId { get; set; }

        [Display(Name = "协议应用子领域")]
        [ForeignKey("SubFieldId")]
        public virtual SysDictionary SubField { get; set; }

        [Required]
        [Display(Name = "协议名称")]
        public string ProtocolName { get; set; }

        [Required]
        [Display(Name = "协议处理模块")]
        public string ProtocolModule { get; set; }

        [Display(Name = "协议自定义段")]
        public virtual string CustomerInfo { get; set; }

        [Required]
        [Display(Name = "协议版本号")]
        public virtual string Version { get; set; }

        [Required]
        [Display(Name = "协议头")]
        public virtual byte[] Head { get; set; }

        [Required]
        [Display(Name = "协议尾")]
        public virtual byte[] Tail { get; set; }

        [Display(Name = "协议包含的结构")]
        public virtual ICollection<ProtocolStructure> ProtocolStructures { get; set; }

        [Display(Name = "协议包含的指令")]
        public virtual ICollection<ProtocolCommand> ProtocolCommands { get; set; }

        [Required]
        [Display(Name = "协议发布时间")]
        public virtual DateTime ReleaseDateTime { get; set; }

        [Required]
        [Display(Name = "校验类型")]
        public virtual string CheckType { get; set; }
    }
}
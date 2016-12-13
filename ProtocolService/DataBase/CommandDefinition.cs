using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SHWDTech.Platform.StorageConstrains.Model;

namespace SHWDTech.Platform.ProtocolService.DataBase
{
    /// <summary>
    /// 指令定义模型
    /// </summary>
    [Serializable]
    public class CommandDefinition : GuidModel
    {
        [Required]
        [Display(Name = "所属协议指令ID")]
        public virtual Guid CommandGuid { get; set; }

        [ForeignKey("CommandGuid")]
        [Display(Name = "所属协议")]
        public virtual ProtocolCommand Command { get; set; }

        [MaxLength(200)]
        [Display(Name = "对应结构名称")]
        public virtual string StructureName { get; set; }

        [Display(Name = "默认值")]
        public virtual byte[] ContentBytes { get; set; }
    }
}

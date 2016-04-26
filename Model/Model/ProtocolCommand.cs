using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace SHWDTech.Platform.Model.Model
{
    /// <summary>
    /// 协议指令模型
    /// </summary>
    [Serializable]
    public class ProtocolCommand : SysModelBase, IProtocolCommand
    {
        [Display(Name = "指令类型编码")]
        public virtual byte[] CommandTypeCode { get; set; }

        [Required]
        [Display(Name = "指令编码")]
        public virtual byte[] CommandCode { get; set; }

        [Required]
        [Display(Name = "指令数据长度")]
        public virtual int CommandBytesLength { get; set; }

        [Required]
        [Display(Name = "指令类型")]
        [MaxLength(50)]
        public virtual string CommandCategory { get; set; }

        [Required]
        [Display(Name = "所属协议ID")]
        public virtual Guid ProtocolId { get; set; }

        [Display(Name = "所属协议")]
        [ForeignKey("ProtocolId")]
        public virtual Protocol Protocol { get; set; }

        [Display(Name = "指令包含数据")]
        public virtual ICollection<CommandData> CommandDatas { get; set; }

        [Display(Name = "指令处理参数")]
        public virtual ICollection<SysConfig> CommandDeliverParamConfigs { get; set; }

        [Display(Name = "指令定义信息")]
        public virtual ICollection<CommandDefinition> CommandDefinitions { get;set; }

        [NotMapped]
        public virtual List<string> CommandDeliverParams
            => CommandDeliverParamConfigs.Count > 0
                ? CommandDeliverParamConfigs.Select(config => config.SysConfigName).ToList()
                : new List<string>();
    }
}
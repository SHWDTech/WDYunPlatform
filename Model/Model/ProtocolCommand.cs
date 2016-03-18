using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;

namespace SHWDTech.Platform.Model.Model
{
    /// <summary>
    /// 协议指令模型
    /// </summary>
    [Serializable]
    public class ProtocolCommand : SysModelBase, IProtocolCommand
    {
        [Display(Name = "指令类型")]
        public byte[] CommandType { get; set; }

        [Required]
        [Display(Name = "指令编码")]
        public byte[] CommandCode { get; set; }

        [Required]
        [Display(Name = "指令数据长度")]
        public int CommandDataLength { get; set; }

        [Required]
        [Display(Name = "所属协议ID")]
        public Guid ProtocolId { get; set; }

        [Display(Name = "所属协议")]
        [ForeignKey("ProtocolId")]
        public Protocol Protocol { get; set; }

        [Required]
        [Display(Name = "指令包含数据")]
        public ICollection<CommandData> CommandDatas { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using SHWDTech.Platform.Model;

namespace SHWDTech.Platform.ProtocolService.DataBase
{
    /// <summary>
    /// 协议指令模型
    /// </summary>
    [Serializable]
    public class ProtocolCommand : GuidModel
    {
        [Display(Name = "指令类型编码")]
        public virtual byte[] CommandTypeCode { get; set; }

        [Required]
        [Display(Name = "指令编码")]
        public virtual byte[] CommandCode { get; set; }

        [Required]
        [Display(Name = "指令发送数据长度")]
        public virtual int SendBytesLength { get; set; }

        [Required]
        [Display(Name = "指令数据接收长度")]
        public virtual int ReceiveBytesLength { get; set; }

        [Required]
        [Display(Name = "指令数据接收最大长度")]
        public virtual int ReceiceMaxBytesLength { get; set; }

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

        [Display(Name = "数据段组合方式")]
        public virtual DataOrderType DataOrderType { get; set; }

        [Display(Name = "指令包含数据")]
        public virtual ICollection<CommandData> CommandDatas { get; set; }

        [Display(Name = "指令处理参数")]
        public virtual string DeliverParams { get; set; }

        [Display(Name = "指令定义信息")]
        public virtual ICollection<CommandDefinition> CommandDefinitions { get; set; }

        [NotMapped]
        public virtual List<string> CommandDeliverParams
        {
            get
            {
                if (_commandDeliverParams != null) return _commandDeliverParams;
                _commandDeliverParams = DeliverParams.Split(',').ToList();

                return _commandDeliverParams;
            }
        }

        private List<string> _commandDeliverParams;
    }
}

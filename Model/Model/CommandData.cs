using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;

namespace SHWDTech.Platform.Model.Model
{
    /// <summary>
    /// 指令数据模型
    /// </summary>
    [Serializable]
    public class CommandData : SysModelBase, ICommandData
    {
        [Required]
        [Display(Name = "数据索引值")]
        public int DataIndex { get; set; }

        [Required]
        [Display(Name = "数据长度")]
        public int DataLength { get; set; }

        [Required]
        [Display(Name = "数据名称")]
        public string DataName { get; set; }

        [Required]
        [Display(Name = "数据类型值")]
        public string DataType { get; set; }

        [Required]
        [Display(Name = "所属指令ID")]
        public Guid CommandId { get; set; }

        [Required]
        [Display(Name = "所属指令")]
        [ForeignKey("CommandId")]
        public ProtocolCommand Command { get; set; }
    }
}

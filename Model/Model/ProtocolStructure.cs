using System;
using System.ComponentModel.DataAnnotations;
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
        [Display(Name = "所属协议")]
        public virtual Protocol Procotol { get; set; }

        [Display(Name = "所属父结构")]
        public virtual ProtocolStructure Parenttructure { get; set; }

        [Display(Name = "协议段名称")]
        [MaxLength(50)]
        public string ComponentName { get; set; }

        [Display(Name = "协议段索引值")]
        public int ComponentIndex { get; set; }

        [Display(Name = "协议段长度")]
        public int ComponentLength { get; set; }

        [Display(Name = "协议段数据长度")]
        public int ComponentDataLength { get; set; }
    }
}
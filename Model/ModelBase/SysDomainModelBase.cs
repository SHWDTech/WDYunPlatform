using System;
using SHWDTech.Platform.Model.IModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SHWDTech.Platform.Model.Model;

namespace SHWDTech.Platform.Model.ModelBase
{
    /// <summary>
    /// 带有域的系统模型基类
    /// </summary>
    [Serializable]
    public class SysDomainModelBase : SysModelBase, ISysDomainModel
    {
        [Required]
        [Display(Name = "所属域ID")]
        public virtual Guid DomainId { get; set; }

        [ForeignKey("DomainId")]
        [Display(Name = "所属域")]
        public virtual Domain Domain { get; set; }
    }
}
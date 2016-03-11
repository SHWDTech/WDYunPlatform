using SHWDTech.Platform.Model.IModel;
using System.ComponentModel.DataAnnotations;
using SHWDTech.Platform.Model.Model;

namespace SHWDTech.Platform.Model.ModelBase
{
    /// <summary>
    /// 带有域的系统模型基类
    /// </summary>
    public class SysDomainModelBase : SysModelBase, ISysDomainModel
    {
        [Required]
        [Display(Name = "所属域")]
        public virtual Domain Domain { get; set; }
    }
}
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SHWDTech.Platform.Model.Model
{
    /// <summary>
    /// 模块模型对象
    /// </summary>
    [Serializable]
    public class Module : SysDomainModelBase, IModule
    {
        [Display(Name = "所属父模块ID")]
        public virtual Guid? ParentModuleId { get; set; }

        [Display(Name = "所属父模块")]
        [ForeignKey("ParentModuleId")]
        public virtual Module ParentModule { get; set; }

        [Required]
        [Display(Name = "是否菜单项")]
        public bool IsMenu { get; set; }

        [Required]
        [Display(Name = "模块层级")]
        public virtual int ModuleLevel { get; set; }

        [Required]
        [Display(Name = "模块名称")]
        [MaxLength(25)]
        public virtual string ModuleName { get; set; }

        [Display(Name = "模块所属控制器")]
        [MaxLength(25)]
        public virtual string Controller { get; set; }

        [Display(Name = "模块所属操作")]
        [MaxLength(25)]
        public virtual string Action { get; set; }

        [Display(Name = "模块所属权限ID")]
        public virtual Guid? PermissionId { get; set; }

        [Display(Name = "模块所属权限")]
        [ForeignKey("PermissionId")]
        public virtual Permission Permission { get; set; }
    }
}
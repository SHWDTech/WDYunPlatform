using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SHWDTech.Platform.Model.Model
{
    /// <summary>
    /// 菜单
    /// </summary>
    [Serializable]
    public class Menu : SysDomainModelBase, IMenu
    {
        [Display(Name = "所属父菜单ID")]
        public virtual Guid? ParentMenuId { get; set; }

        [Display(Name = "所属父菜单")]
        [ForeignKey("ParentMenuId")]
        public virtual Menu ParentMenu { get; set; }

        [Required]
        [Display(Name = "菜单层级")]
        public virtual int MenuLevel { get; set; }

        [Required]
        [Display(Name = "菜单名称")]
        [MaxLength(25)]
        public virtual string MenuName { get; set; }

        [Required]
        [Display(Name = "菜单所属控制器")]
        [MaxLength(25)]
        public virtual string Controller { get; set; }

        [Display(Name = "菜单所属操作")]
        [MaxLength(25)]
        [Required]
        public virtual string Action { get; set; }

        [Required]
        [Display(Name = "菜单所属权限ID")]
        public virtual Guid PermissionId { get; set; }

        [Display(Name = "菜单所属权限")]
        [ForeignKey("PermissionId")]
        public virtual Permission Permission { get; set; }
    }
}
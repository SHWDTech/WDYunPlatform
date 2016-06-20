using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SHWDTech.Platform.Model.Enums;

namespace SHWDTech.Platform.Model.Model
{
    /// <summary>
    /// 权限
    /// </summary>
    [Serializable]
    public class Permission : SysDomainModelBase, IPermission
    {
        [Required]
        [Display(Name = "权限名称")]
        [MaxLength(25)]
        public virtual string PermissionName { get; set; }

        [Display(Name = "父级权限ID")]
        public virtual Guid? ParentPermissionId { get; set; }

        [Display(Name = "父级权限")]
        [ForeignKey("ParentPermissionId")]
        public virtual Permission ParentPermission { get; set; }

        [Display(Name = "权限类型")]
        public virtual PermissionType Type { get; set; }

        [Display(Name = "拥有权限的角色")]
        public virtual ICollection<WdRole> Roles { get; set; } 

        [Display(Name = "拥有权限的用户")]
        public virtual ICollection<WdUser> Users { get; set; }
    }
}
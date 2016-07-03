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
    /// 角色
    /// </summary>
    [Serializable]
    public class WdRole : SysDomainModelBase, IWdRole
    {
        [Display(Name = "父级角色ID")]
        public virtual Guid? ParentRoleId { get; set; }

        [Display(Name = "父级角色")]
        [ForeignKey("ParentRoleId")]
        public virtual WdRole ParentRole { get; set; }

        [Required]
        [Display(Name = "角色名")]
        [MaxLength(25)]
        public virtual string RoleName { get; set; }

        [Display(Name = "包含用户")]
        public virtual ICollection<WdUser> Users { get; set; } = new List<WdUser>();

        [Required]
        [Display(Name = "角色状态")]
        public virtual RoleStatus Status { get; set; }

        [Display(Name = "角色描述")]
        public virtual string Comments { get; set; }

        [Display(Name = "包含权限")]
        public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();
    }
}
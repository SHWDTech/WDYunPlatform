using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SHWDTech.Platform.Model.Model
{
    /// <summary>
    /// 角色
    /// </summary>
    [Serializable]
    public class WdRole : SysDomainModelBase, IWdRole
    {
        [Display(Name = "父级角色")]
        public WdRole ParentRole { get; set; }

        [Required]
        [Display(Name = "用户组名")]
        [MaxLength(25)]
        public string RoleName { get; set; }

        [Display(Name = "包含用户")]
        public List<WdUser> Users { get; set; }

        [Display(Name = "包含权限")]
        public List<Permission> Permissions { get; set; }
    }
}
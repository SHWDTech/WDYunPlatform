using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;

namespace SHWDTech.Platform.Model.Model
{
    [Serializable]
    public class Role : SysDomainModelBase, IRole
    {
        [Required]
        public SysDomain RoleDomain { get; set; }

        [Display(Name = "父级角色")]
        public Role ParentRole { get; set; }

        [Required]
        [Display(Name = "用户组名")]
        [MaxLength(25)]
        public string RoleName { get; set; }

        [Display(Name = "是否启用")]
        public bool IsEnabled { get; set; }

        [Display(Name = "包含用户")]
        public List<User> Users { get; set; }

        [Display(Name = "包含权限")]
        public List<Permission> Permissions { get; set; }
    }
}

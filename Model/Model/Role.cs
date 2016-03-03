﻿using SHWDTech.Platform.Model.IModel;
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
    public class Role : SysDomainModelBase, IRole
    {
        [Display(Name = "父级角色")]
        public Role ParentRole { get; set; }

        [Required]
        [Display(Name = "用户组名")]
        [MaxLength(25)]
        public string RoleName { get; set; }

        [Display(Name = "包含用户")]
        public List<User> Users { get; set; }

        [Display(Name = "包含权限")]
        public List<Permission> Permissions { get; set; }
    }
}
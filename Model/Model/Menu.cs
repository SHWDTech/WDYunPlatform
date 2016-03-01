﻿using System;
using System.ComponentModel.DataAnnotations;
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;

namespace SHWDTech.Platform.Model.Model
{
    [Serializable]
    public class Menu : SysDomainModelBase, IMenu
    {
        [Required]
        [Display(Name = "菜单所属域")]
        public SysDomain MenuDomain { get; set; }

        [Display(Name = "所属父菜单")]
        public Menu ParentMenu { get; set; }

        [Required]
        [Display(Name = "菜单层级")]
        public int MenuLevel { get; set; }

        [Required]
        [Display(Name = "菜单名称")]
        [MaxLength(25)]
        public string MenuName { get; set; }

        [Required]
        [Display(Name = "菜单所属控制器")]
        [MaxLength(25)]
        public string Controller { get; set; }

        [Display(Name = "菜单所属操作")]
        [MaxLength(25)]
        [Required]
        public string Action { get; set; }

        [Required]
        [Display(Name = "菜单所属权限")]
        public Permission Permission { get; set; }
    }
}

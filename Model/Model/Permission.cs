﻿using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    }
}
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;
using System;
using System.ComponentModel.DataAnnotations;

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

        [Display(Name = "父级权限")]
        public virtual Permission ParentPermission { get; set; }
    }
}
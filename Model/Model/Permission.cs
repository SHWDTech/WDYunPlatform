using System;
using System.ComponentModel.DataAnnotations;
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;

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
        public string PermissionName { get; set; }
    }
}

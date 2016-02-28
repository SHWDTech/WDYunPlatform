using System;
using System.ComponentModel.DataAnnotations;
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;

namespace SHWDTech.Platform.Model.Model
{
    [Serializable]
    public class Permission : SysModelBase, IPermission
    {
        [Key]
        public int PermissionId { get; set; }

        [Required]
        public SysDomain PermissionDomain { get; set; }


        [Required]
        [Display(Name = "权限名称")]
        [MaxLength(25)]
        public string PermissionName { get; set; }
    }
}

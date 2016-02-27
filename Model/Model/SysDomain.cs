using System;
using System.ComponentModel.DataAnnotations;
using SHWDTech.Platform.Model.Interface;
using SHWDTech.Platform.Model.ModelBase;

namespace SHWDTech.Platform.Model.Model
{
    [Serializable]
    public class SysDomain :SysModelBase, ISysDomain
    {
        [Key]
        [Display(Name = "域ID")]
        public Guid DomainId { get; set; }

        [Required]
        [Display(Name = "域名称")]
        [MaxLength(50)]
        public string DomainName { get; set; }

        [Display(Name = "是否启用")]
        public bool IsEnabled { get; set; }

        [Display(Name = "域类型")]
        [MaxLength(25)]
        public string DomianType { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;

namespace SHWDTech.Platform.Model.Model
{
    [Serializable]
    public class SysConfig: SysModelBase, ISysConfig
    {
        [Required]
        [Display(Name = "系统配置名称")]
        [MaxLength(25)]
        public string SysConfigName { get; set; }

        [Required]
        [Display(Name = "系统配置类型")]
        [MaxLength(25)]
        public string SysConfigType { get; set; }

        [Required]
        [Display(Name = "系统配置值")]
        [MaxLength(200)]
        public string SysConfigValue { get; set; }
    }
}

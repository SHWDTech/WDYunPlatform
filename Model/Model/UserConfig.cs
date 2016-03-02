using System;
using System.ComponentModel.DataAnnotations;
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;

namespace SHWDTech.Platform.Model.Model
{
    /// <summary>
    /// 用户配置
    /// </summary>
    [Serializable]
    public class UserConfig: SysDomainModelBase, IUserConfig
    {
        [Display(Name = "用户配置名称")]
        [Required]
        [MaxLength(25)]
        public string UserConfigName { get; set; }

        [Required]
        [Display(Name = "用户配置类型")]
        [MaxLength(25)]
        public string UserConfigType { get; set; }

        [Required]
        [Display(Name = "用户配置值")]
        [MaxLength(200)]
        public string UserConfigValue { get; set; }
    }
}

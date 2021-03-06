﻿using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SHWDTech.Platform.Model.Model
{
    /// <summary>
    /// 用户配置
    /// </summary>
    [Serializable]
    public class UserConfig : SysDomainModelBase, IUserConfig
    {
        [Display(Name = "用户配置名称")]
        [Required]
        [MaxLength(25)]
        public virtual string UserConfigName { get; set; }

        [Required]
        [Display(Name = "用户配置类型")]
        [MaxLength(25)]
        public virtual string UserConfigType { get; set; }

        [Required]
        [Display(Name = "用户配置值")]
        [MaxLength(200)]
        public virtual string UserConfigValue { get; set; }

        [Display(Name = "父级用户配置项ID")]
        public Guid? ParentUserConfigId { get; set; }

        [Display(Name = "父级用户配置项")]
        [ForeignKey("ParentUserConfigId")]
        public UserConfig ParentUserConfig { get; set; }
    }
}
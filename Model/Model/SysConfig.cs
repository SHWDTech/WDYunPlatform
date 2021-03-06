﻿using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SHWDTech.Platform.Model.Model
{
    /// <summary>
    /// 系统配置
    /// </summary>
    [Serializable]
    public class SysConfig : SysModelBase, ISysConfig
    {
        [Required]
        [Display(Name = "系统配置名称")]
        [MaxLength(25)]
        public virtual string SysConfigName { get; set; }

        [Required]
        [Display(Name = "系统配置类型")]
        [MaxLength(25)]
        public virtual string SysConfigType { get; set; }

        [Required]
        [Display(Name = "系统配置值")]
        [MaxLength(200)]
        public virtual string SysConfigValue { get; set; }

        [Display(Name = "所属系统配置ID")]
        public virtual Guid? ParentSysConfigId { get; set; }

        [Display(Name = "所属系统配置")]
        [ForeignKey("ParentSysConfigId")]
        public virtual SysConfig ParentSysConfig { get; set; }

        [Display(Name = "相关协议指令")]
        [ForeignKey("CommandId")]

        public virtual ICollection<ProtocolCommand> ProtocolCommands { get; set; }
    }
}
﻿using System;
using System.ComponentModel.DataAnnotations;
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;

namespace SHWDTech.Platform.Model.Model
{
    /// <summary>
    /// 协议
    /// </summary>
    [Serializable]
    public class Protocol : SysModelBase, IProtocol
    {
        [Required]
        [Display(Name = "协议应用领域")]
        public SysDictionary Field { get; set; }

        [Required]
        [Display(Name = "协议应用子领域")]
        public SysDictionary SubField { get; set; }

        [Required]
        [Display(Name = "协议自定义段")]
        public string CustomerInfo { get; set; }

        [Required]
        [Display(Name = "协议版本号")]
        public string Version { get; set; }

        [Required]
        [Display(Name = "协议发布时间")]
        public DateTime ReleaseDateTime { get; set; }

        [Required]
        [Display(Name = "协议结构")]
        public string ProtocolStructure { get; set; }
    }
}
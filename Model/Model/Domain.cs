﻿using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;
using System;
using System.ComponentModel.DataAnnotations;

namespace SHWDTech.Platform.Model.Model
{
    /// <summary>
    /// 域
    /// </summary>
    [Serializable]
    public class Domain : SysModelBase, IDomain
    {
        [Required]
        [Display(Name = "域名称")]
        [MaxLength(50)]
        public virtual string DomainName { get; set; }

        [Display(Name = "域类型")]
        [MaxLength(25)]
        public virtual string DomianType { get; set; }
    }
}
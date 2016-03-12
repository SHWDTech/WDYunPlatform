using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SHWDTech.Platform.Model.Model
{
    /// <summary>
    /// 固件
    /// </summary>
    [Serializable]
    public class Firmware : SysModelBase, IFirmware
    {
        [Required]
        [Display(Name = "固件名称")]
        public virtual string FirmwareName { get; set; }

        [Display(Name = "固件支持的协议类型")]
        public virtual ICollection<Protocol> Protocols { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SHWDTech.Platform.StorageConstrains.Model;

namespace SHWDTech.Platform.ProtocolService.DataBase
{
    /// <summary>
    /// 固件
    /// </summary>
    [Serializable]
    public class Firmware : GuidModel
    {
        [Required]
        [Display(Name = "固件名称")]
        public virtual string FirmwareName { get; set; }

        [Display(Name = "固件所属固件集")]
        public virtual ICollection<FirmwareSet> FirmwareSets { get; set; } = new List<FirmwareSet>();

        [Display(Name = "固件支持的协议类型")]
        public virtual ICollection<Protocol> Protocols { get; set; } = new List<Protocol>();
    }
}

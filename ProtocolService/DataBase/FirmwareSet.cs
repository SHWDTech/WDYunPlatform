using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SHWDTech.Platform.StorageConstrains.Model;

namespace SHWDTech.Platform.ProtocolService.DataBase
{
    /// <summary>
    /// 固件集
    /// </summary>
    [Serializable]
    public class FirmwareSet : GuidModel
    {
        [Required]
        [Display(Name = "固件集名称")]
        public virtual string FirmwareSetName { get; set; }

        [Required]
        [Display(Name = "固件集包含固件列表")]
        public virtual ICollection<Firmware> Firmwares { get; set; } = new List<Firmware>();
    }
}

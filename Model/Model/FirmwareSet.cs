using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.ModelBase;

namespace SHWDTech.Platform.Model.Model
{
    /// <summary>
    /// 固件集
    /// </summary>
    [Serializable]
    public class FirmwareSet : SysModelBase, IFirmwareSet
    {
        [Required]
        [Display(Name = "固件集名称")]
        public string FirmwareSetName { get; set; }

        [Required]
        [Display(Name = "固件集包含固件列表")]
        public List<Firmware> Firmwares { get; set; }
    }
}

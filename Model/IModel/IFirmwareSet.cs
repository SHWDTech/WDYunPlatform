using System.Collections.Generic;
using SHWDTech.Platform.Model.Model;

namespace SHWDTech.Platform.Model.IModel
{
    /// <summary>
    /// 固件集接口
    /// </summary>
    public interface IFirmwareSet : ISysModel
    {
        /// <summary>
        /// 固件集名称
        /// </summary>
        string FirmwareSetName { get; set; }

        /// <summary>
        /// 固件集包含固件列表
        /// </summary>
        List<Firmware> Firmwares { get; set; } 
    }
}

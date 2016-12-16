using System.Collections.Generic;

namespace SHWDTech.Platform.ProtocolService.DataBase
{
    public interface IFirmwareSet
    {
        /// <summary>
        /// 固件集名称
        /// </summary>
        string FirmwareSetName { get; set; }

        /// <summary>
        /// 固件集包含的固件列表
        /// </summary>
        ICollection<Firmware> Firmwares { get; set; }
    }
}

using SHWDTech.Platform.Model.Model;
using System.Collections.Generic;

namespace SHWDTech.Platform.Model.IModel
{
    /// <summary>
    /// 固件接口
    /// </summary>
    public interface IFirmware : ISysModel
    {
        /// <summary>
        /// 固件名称
        /// </summary>
        string FirmwareName { get; set; }

        /// <summary>
        /// 包含该固件的固件集
        /// </summary>
        ICollection<FirmwareSet> FirmwareSets { get; set; }

        /// <summary>
        /// 固件支持的协议类型
        /// </summary>
        ICollection<Protocol> Protocols { get; set; }
    }
}
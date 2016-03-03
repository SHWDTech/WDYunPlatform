using System.Collections.Generic;
using SHWDTech.Platform.Model.Model;

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
        /// 固件支持的协议类型
        /// </summary>
        List<Protocol> Protocols { get; set; }
    }
}

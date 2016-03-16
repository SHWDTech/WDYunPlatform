using System.Collections.Generic;
using SHWDTech.Platform.Model.Model;

namespace Platform.Process.IProcess
{
    /// <summary>
    /// 协议信息处理器接口
    /// </summary>
    public interface IProtocolCodingProcess
    {
        IList<Firmware> GetAllFirmwares();
    }
}

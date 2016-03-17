using System;
using System.Collections.Generic;
using SHWDTech.Platform.Model.Model;

namespace Platform.Process.IProcess
{
    /// <summary>
    /// 协议信息处理器接口
    /// </summary>
    public interface IProtocolCodingProcess
    {
        /// <summary>
        /// （同步）读取设备相关协议信息及协议结构
        /// </summary>
        /// <param name="deviceGuid"></param>
        /// <returns></returns>
        IList<Protocol> GetDeviceProtocolsFullLoaded(Guid deviceGuid);
    }
}

using System;

namespace SHWDTech.Platform.ProtocolCoding.Coding
{
    /// <summary>
    /// 表示协议包
    /// </summary>
    public interface IProtocolPackage
    {
        /// <summary>
        /// 设备GUID
        /// </summary>
        Guid DeviceGuid { get; set; }

        /// <summary>
        /// 协议接收时间
        /// </summary>
        DateTime ReceiveDateTime { get; set; } 

        Component this[string name] { get; set; }
    }
}

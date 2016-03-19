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

        /// <summary>
        /// 协议数据段
        /// </summary>
        PackageComponent DataComponent { get; }

        /// <summary>
        /// 获取指定名称的数据段
        /// </summary>
        /// <param name="name">数据段名称</param>
        /// <returns>指定名称的数据段</returns>
        PackageComponent this[string name] { get; set; }
    }
}

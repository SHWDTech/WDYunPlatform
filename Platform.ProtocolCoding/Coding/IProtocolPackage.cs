using System;
using SHWDTech.Platform.Model.IModel;

namespace SHWDTech.Platform.ProtocolCoding.Coding
{
    /// <summary>
    /// 表示协议包
    /// </summary>
    public interface IProtocolPackage
    {
        /// <summary>
        /// 是否已完成编解码
        /// </summary>
        bool Finalized { get; }

        /// <summary>
        /// 协议包长度
        /// </summary>
        int PackageLenth { get; }

        /// <summary>
        /// 协议包所属设备
        /// </summary>
        IDevice Device { get; set; }

        /// <summary>
        /// 协议接收时间
        /// </summary>
        DateTime ReceiveDateTime { get; set; }

        /// <summary>
        /// 所属协议
        /// </summary>
        IProtocol Protocol { get; set; }

        /// <summary>
        /// 所属指令
        /// </summary>
        IProtocolCommand Command { get; set; }

        /// <summary>
        /// 协议数据段
        /// </summary>
        IPackageComponent DataComponent { get; }

        /// <summary>
        /// 获取指定名称的数据段
        /// </summary>
        /// <param name="name">数据段名称</param>
        /// <returns>指定名称的数据段</returns>
        IPackageComponent this[string name] { get; set; }

        /// <summary>
        /// 数据包处理参数
        /// </summary>
        string DeliverParams { get; set; }

        /// <summary>
        /// 完成协议包的编解码
        /// </summary>
        void Finalization();
    }
}

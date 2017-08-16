using System;
using System.Collections.Generic;
using SHWDTech.Platform.ProtocolService.DataBase;

namespace SHWDTech.Platform.ProtocolService.ProtocolEncoding
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
        IClientSource ClientSource { get; set; }

        /// <summary>
        /// 协议接收时间
        /// </summary>
        DateTime ReceiveDateTime { get; set; }

        /// <summary>
        /// 协议包数据记录ID
        /// </summary>
        IProtocolData ProtocolData { get; set; }

        /// <summary>
        /// 所属协议
        /// </summary>
        IProtocol Protocol { get; set; }

        /// <summary>
        /// 所属指令
        /// </summary>
        IProtocolCommand Command { get; set; }

        /// <summary>
        /// 数据段总数
        /// </summary>
        int DataComponentCount { get; }

        /// <summary>
        /// 设备NODEID号
        /// </summary>
        string DeviceNodeId { get; set; }

        /// <summary>
        /// 数据包处理参数
        /// </summary>
        List<string> DeliverParams { get; }

        /// <summary>
        /// 协议包状态
        /// </summary>
        PackageStatus Status { get; set; }

        /// <summary>
        /// 设置协议数据信息
        /// </summary>
        void SetupProtocolData();

        /// <summary>
        /// 获取数据包字节流
        /// </summary>
        /// <returns></returns>
        byte[] GetBytes();

        /// <summary>
        /// 完成协议包的编解码
        /// </summary>
        void Finalization();

        /// <summary>
        /// 协议包分解信息
        /// </summary>
        string PackageComponentFactors { get; }
    }
}

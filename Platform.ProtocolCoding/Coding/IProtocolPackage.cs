using System;
using System.Collections.Generic;
using SHWDTech.Platform.Model.IModel;
using SHWDTech.Platform.Model.Model;
using SHWDTech.Platform.ProtocolCoding.Enums;

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
        Device Device { get; set; }

        /// <summary>
        /// 协议接收时间
        /// </summary>
        DateTime ReceiveDateTime { get; set; }

        /// <summary>
        /// 所属协议
        /// </summary>
        Protocol Protocol { get; set; }

        /// <summary>
        /// 所属指令
        /// </summary>
        IProtocolCommand Command { get; set; }

        /// <summary>
        /// 获取指定名称的数据段
        /// </summary>
        /// <param name="name">数据段名称</param>
        /// <returns>指定名称的数据段</returns>
        IPackageComponent this[string name] { get; set; }

        /// <summary>
        /// 数据包处理参数
        /// </summary>
        List<string> DeliverParams { get; }

        /// <summary>
        /// 协议包状态
        /// </summary>
        PackageStatus Status { get; set; }

        /// <summary>
        /// 添加数据段数据
        /// </summary>
        /// <param name="component"></param>
        void AppendData(IPackageComponent component);

        /// <summary>
        /// 获取数据包字节流
        /// </summary>
        /// <returns></returns>
        byte[] GetBytes();

        /// <summary>
        /// 完成协议包的编解码
        /// </summary>
        void Finalization();
    }
}

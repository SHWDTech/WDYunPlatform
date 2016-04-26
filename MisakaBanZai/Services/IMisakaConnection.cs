using System.Collections.Generic;
using System.Net;
using MisakaBanZai.Models;

namespace MisakaBanZai.Services
{
    /// <summary>
    /// 通信对象通用接口
    /// </summary>
    public interface IMisakaConnection
    {
        /// <summary>
        /// 连接名称
        /// </summary>
        string ConnectionName { get; }

        /// <summary>
        /// 连接对象名称
        /// </summary>
        string TargetConnectionName { get; set; }

        /// <summary>
        /// 连接类型
        /// </summary>
        string ConnectionType { get; }

        /// <summary>
        /// 连接对象
        /// </summary>
        object ConnObject { get; }

        /// <summary>
        /// 连接IP地址
        /// </summary>
        string IpAddress { get; }

        /// <summary>
        /// 端口号
        /// </summary>
        int Port { get; }

        /// <summary>
        /// 指示通信对象是否已经连接上
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// 所属窗体
        /// </summary>
        IMisakaConnectionManagerWindow ParentWindow { get; set; }

        /// <summary>
        /// 接收数据事件
        /// </summary>
        event ClientReceivedDataEventHandler ClientReceivedDataEvent;

        /// <summary>
        /// 断开连接事件
        /// </summary>
        event ClientDisconnectEventHandler ClientDisconnectEvent;

        /// <summary>
        /// 数据发送事件
        /// </summary>
        event DataSendEventHandler DataSendEvent;

        /// <summary>
        /// 输出套接字字节流
        /// </summary>
        /// <returns></returns>
        byte[] OutPutSocketBytes();

        /// <summary>
        /// 数据处理缓存
        /// </summary>
        IList<byte> ProcessBuffer { get; }

        /// <summary>
        /// 开始连接
        /// </summary>
        bool Connect(IPAddress ipAddress, int port);

        /// <summary>
        /// 发送字节流
        /// </summary>
        /// <param name="bytes"></param>
        int Send(byte[] bytes);

        /// <summary>
        /// 关闭连接
        /// </summary>
        bool Close();
    }
}

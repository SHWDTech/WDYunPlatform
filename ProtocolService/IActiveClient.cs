using System;

namespace SHWDTech.Platform.ProtocolService
{
    public interface IActiveClient
    {
        /// <summary>
        /// 客户端数据接收事件
        /// </summary>
        event ActiveClientEventHandler ClientReceivedData;

        /// <summary>
        /// 客户端断开事件
        /// </summary>
        event ActiveClientEventHandler ClientDisconnect;

        /// <summary>
        /// 客户端授权事件
        /// </summary>
        event ActiveClientEventHandler ClientAuthenticated;

        /// <summary>
        /// 客户端解码失败事件
        /// </summary>
        event ActiveClientEventHandler ClientDecodeFalied;

        /// <summary>
        /// 客户端地址
        /// </summary>
        string ClientAddress { get; }

        /// <summary>
        /// 客户端身份码
        /// </summary>
        string ClientIdentity { get; }

        /// <summary>
        /// 客户端GUID
        /// </summary>
        Guid ClientGuid { get; }

        /// <summary>
        /// 指示客户端是否处于连接状态
        /// </summary>
        bool IsConnected { get; }

        /// <summary>
        /// 客户端授权状态
        /// </summary>
        AuthenticationStatus AuthStatus { get; }

        /// <summary>
        /// 客户端数据源
        /// </summary>
        IClientSource ClientSource { get; set; }

        /// <summary>
        /// 客户端最后一次活跃时间
        /// </summary>
        DateTime LastAliveDateTime { get; }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="protocolBytes">协议包字节流</param>
        void Send(byte[] protocolBytes);
    }
}

﻿using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using SHWDTech.Platform.ProtocolCoding;
using SHWDTech.Platform.ProtocolCoding.MessageQueueModel;
using WdTech_Protocol_AdminTools.Services;

namespace WdTech_Protocol_AdminTools.TcpCore
{
    /// <summary>
    /// 活动客户端管理器
    /// </summary>
    public class ActiveClientManager
    {
        /// <summary>
        /// 未认证的客户端连接
        /// </summary>
        // ReSharper disable once CollectionNeverQueried.Local
        private readonly List<TcpClientManager> _clientSockets
            = new List<TcpClientManager>();

        /// <summary>
        /// 添加一个客户端
        /// </summary>
        /// <param name="client"></param>
        public void AddClient(Socket client)
        {
            var tcpClientManager = new TcpClientManager(client) { ReceiverName = $"UnIdentified - {client.RemoteEndPoint}" };
            tcpClientManager.ClientDisconnectEvent += ClientDisconnected;
            tcpClientManager.ClientAuthenticationEvent += ClientAuthenticationed;
            client.BeginReceive(tcpClientManager.ReceiveBuffer, SocketFlags.None, tcpClientManager.Received, client);

            lock (_clientSockets)
            {
                _clientSockets.Add(tcpClientManager);
            }
        }

        /// <summary>
        /// 客户端断开连接事件
        /// </summary>
        /// <param name="tcpClient">客户端连接</param>
        private void ClientDisconnected(TcpClientManager tcpClient)
        {
            AdminReportService.Instance.Info($"客户端连接断开，客户端信息：{tcpClient.ReceiverName}");
            CommunicationServices.AliveConnection -= 1;
        }

        private void ClientAuthenticationed(TcpClientManager tcpClient)
        {
            AdminReportService.Instance.Info($"客户端授权通过，客户端设备NODEID：{tcpClient.ClientDevice.DeviceNodeId}");
        }

        /// <summary>
        /// 发送指令
        /// </summary>
        /// <param name="message">指令消息</param>
        public void SendCommand(CommandMessage message)
        {
            TcpClientManager device;
            lock (_clientSockets)
            {
                device = _clientSockets.FirstOrDefault(client => client.DeviceGuid == message.DeviceGuid);
            }

            if (device == null) return;

            var command = ProtocolInfoManager.GetCommand(message.CommandGuid);

            if (command == null) return;

            device.Send(command, message.Params);
        }
    }
}

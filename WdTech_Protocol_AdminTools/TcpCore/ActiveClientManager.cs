using System;
using System.Collections.Generic;
using System.Net.Sockets;
using SHWDTech.Platform.Model.IModel;

namespace WdTech_Protocol_AdminTools.TcpCore
{
    /// <summary>
    /// 活动客户端管理器
    /// </summary>
    public class ActiveClientManager
    {
        /// <summary>
        /// 客户端连接
        /// </summary>
        private readonly Dictionary<string, TcpClientReceiver> _clientSockets = new Dictionary<string, TcpClientReceiver>();

        /// <summary>
        /// 已认证的客户端连接
        /// </summary>
        private readonly Dictionary<Guid, TcpClientReceiver> _authedClientSockets = new Dictionary<Guid, TcpClientReceiver>(); 

        /// <summary>
        /// 添加一个客户端
        /// </summary>
        /// <param name="client"></param>
        public void AddClient(Socket client)
        {
            var receiver = new TcpClientReceiver(this, client) {ReceiverName = "UnIdentified - " + client.LocalEndPoint};
            client.BeginReceive(receiver.ReceiveBuffer, SocketFlags.None, receiver.Received, client);

            if (!_clientSockets.ContainsKey(receiver.ReceiverName)) _clientSockets.Add(receiver.ReceiverName, receiver);
        }

        /// <summary>
        /// 身份授权操作
        /// </summary>
        public void Authentication(TcpClientReceiver receiver, IDevice device)
        {
            if (_authedClientSockets.ContainsKey(device.Id))
            {
                _authedClientSockets[device.Id].Close();
                _authedClientSockets[device.Id] = receiver;
            }
            else
            {
                _authedClientSockets.Add(device.Id, receiver);
            }
        }

        public void ResetClient(TcpClientReceiver receiver)
        {
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using SHWDTech.Platform.ProtocolCoding;
using SHWDTech.Platform.Utility;

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
        private readonly Dictionary<string, TcpClientManager> _unAuthedClientSockets 
            = new Dictionary<string, TcpClientManager>();

        /// <summary>
        /// 已认证的客户端连接
        /// </summary>
        private readonly Dictionary<Guid, TcpClientManager> _authedClientSockets 
            = new Dictionary<Guid, TcpClientManager>();

        /// <summary>
        /// 设备认证线程
        /// </summary>
        private readonly Thread _authenticationThread;

        /// <summary>
        /// 协议解析线程
        /// </summary>
        private readonly Thread _protocolCodingThread;

        public ActiveClientManager()
        {
            _authenticationThread = new Thread(Authentication) {IsBackground = true};
            _protocolCodingThread = new Thread(ProtocolCoding) {IsBackground = true};
            _protocolCodingThread.Start();
        }

        /// <summary>
        /// 添加一个客户端
        /// </summary>
        /// <param name="client"></param>
        public void AddClient(Socket client)
        {
            var tcpClientManager = new TcpClientManager(client) {ReceiverName = "UnIdentified - " + client.LocalEndPoint};
            client.BeginReceive(tcpClientManager.ReceiveBuffer, SocketFlags.None, tcpClientManager.Received, client);

            if (!_unAuthedClientSockets.ContainsKey(tcpClientManager.ReceiverName))
            {
                _unAuthedClientSockets.Add(tcpClientManager.ReceiverName, tcpClientManager);
            }

            if (!_authenticationThread.IsAlive)
            {
                _authenticationThread.Start();
            }
        }

        /// <summary>
        /// 关闭所有设备连接，并停止解析协议。
        /// </summary>
        public void Stop()
        {
            try
            {
                _protocolCodingThread.Abort();
            }
            catch (Exception ex)
            {
                LogService.Instance.Error("停止协议处理线程失败。", ex);
            }
        }

        /// <summary>
        /// 身份授权操作
        /// </summary>
        private void Authentication()
        {
            while (_unAuthedClientSockets.Count > 0)
            {
                foreach (var clientManager in _unAuthedClientSockets.Where(clientManager => clientManager.Value.Authentication()))
                {
                    _unAuthedClientSockets.Remove(clientManager.Key);
                    _authedClientSockets.Add(clientManager.Value.DeviceGuid, clientManager.Value);
                }
            }
        }

        private void ProtocolCoding()
        {
            while (CommunicationServices.IsStart)
            {
                foreach (var package in _authedClientSockets.Select(clientSocket => clientSocket.Value).Select(client => client.Decode()))
                {
                    PackageDeliver.Deliver(package);
                }
            }
        }
    }
}

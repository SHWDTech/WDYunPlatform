﻿using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using WdTech_Protocol_AdminTools.Services;

namespace WdTech_Protocol_AdminTools.TcpCore
{
    /// <summary>
    /// 活动客户端管理器
    /// </summary>
    public class ActiveClientManager
    {
        public ActiveClientManager()
        {
            var bufferProcessThread = new Thread(ProcessBuffer);
            bufferProcessThread.Start();
        }

        private bool _isClosing;

        /// <summary>
        /// 未认证的客户端连接
        /// </summary>
        private readonly Dictionary<string, TcpClientManager> _clientSockets
            = new Dictionary<string, TcpClientManager>();

        /// <summary>
        /// 添加一个客户端
        /// </summary>
        /// <param name="client"></param>
        public void AddClient(Socket client)
        {
            var tcpClientManager = new TcpClientManager(client) { ReceiverName = $"UnIdentified - {client.LocalEndPoint}"};
            tcpClientManager.ClientDisconnectEvent += ClientDisconnected;
            tcpClientManager.ClientAuthenticationEvent += ClientAuthentication;
            client.BeginReceive(tcpClientManager.ReceiveBuffer, SocketFlags.None, tcpClientManager.Received, client);

            lock (_clientSockets)
            {
                if (!_clientSockets.ContainsKey(tcpClientManager.ReceiverName))
                {
                    _clientSockets.Add(tcpClientManager.ReceiverName, tcpClientManager);
                }
            }
        }

        /// <summary>
        /// 客户端断开连接事件
        /// </summary>
        /// <param name="tcpclient"></param>
        private static void ClientDisconnected(TcpClientManager tcpclient)
        {
            ReportService.Instance.Info($"客户端连接断开，客户端信息：{tcpclient.ReceiverName}");
        }

        /// <summary>
        /// 客户端授权通过事件
        /// </summary>
        /// <param name="tcpclient"></param>
        private void ClientAuthentication(TcpClientManager tcpclient)
        {
            lock (_clientSockets)
            {
                var client = _clientSockets.FirstOrDefault(obj => obj.Value == tcpclient);
                if (client.Value == null) return;

                var tcpManager = client.Value;
                _clientSockets.Remove(client.Key);
                _clientSockets.Add(tcpManager.ReceiverName, tcpManager);
            }
        }

        /// <summary>
        /// 处理数据缓存
        /// </summary>
        private void ProcessBuffer()
        {
            while (!_isClosing)
            {
                lock (_clientSockets)
                {
                    if (_clientSockets.Count <= 0) continue;

                    foreach (var tcpClientManager in _clientSockets)
                    {
                        tcpClientManager.Value.Process();
                    }
                }
            }
        }

        /// <summary>
        /// 停止客户端管理
        /// </summary>
        public void Stop()
        {
            _isClosing = true;
        }
    }
}

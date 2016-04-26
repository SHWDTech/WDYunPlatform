using System.Collections.Generic;
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

        /// <summary>
        /// 是否正在结束程序
        /// </summary>
        private bool _isClosing;

        /// <summary>
        /// 未认证的客户端连接
        /// </summary>
        private readonly List<TcpClientManager> _clientSockets
            = new List<TcpClientManager>();

        /// <summary>
        /// 添加一个客户端
        /// </summary>
        /// <param name="client"></param>
        public void AddClient(Socket client)
        {
            var tcpClientManager = new TcpClientManager(client) { ReceiverName = $"UnIdentified - {client.RemoteEndPoint}"};
            tcpClientManager.ClientDisconnectEvent += ClientDisconnected;
            client.BeginReceive(tcpClientManager.ReceiveBuffer, SocketFlags.None, tcpClientManager.Received, client);

            _clientSockets.Add(tcpClientManager);
        }

        /// <summary>
        /// 客户端断开连接事件
        /// </summary>
        /// <param name="tcpclient"></param>
        private static void ClientDisconnected(TcpClientManager tcpclient)
        {
            AdminReportService.Instance.Info($"客户端连接断开，客户端信息：{tcpclient.ReceiverName}");
        }

        /// <summary>
        /// 处理数据缓存
        /// </summary>
        private void ProcessBuffer()
        {
            while (!_isClosing)
            {
                // ReSharper disable once ForCanBeConvertedToForeach
                for (var i = 0; i < _clientSockets.Count; i++)
                {
                    _clientSockets[i].Process();
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

using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;

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
            var tcpClientManager = new TcpClientManager(client) {ReceiverName = "UnIdentified - " + client.LocalEndPoint};
            client.BeginReceive(tcpClientManager.ReceiveBuffer, SocketFlags.None, tcpClientManager.Received, client);

            if (!_clientSockets.ContainsKey(tcpClientManager.ReceiverName))
            {
                _clientSockets.Add(tcpClientManager.ReceiverName, tcpClientManager);
            }
        }

        /// <summary>
        /// 处理数据缓存
        /// </summary>
        private void ProcessBuffer()
        {
            while (!_isClosing)
            {
                foreach (var tcpClientManager in _clientSockets)
                {
                    tcpClientManager.Value.Process();

                    Thread.Sleep(10);
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

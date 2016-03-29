using System.Collections.Generic;
using System.Net.Sockets;

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
    }
}

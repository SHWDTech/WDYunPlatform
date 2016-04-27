using System.Collections.Generic;
using System.Net.Sockets;
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
            var tcpClientManager = new TcpClientManager(client) { ReceiverName = $"UnIdentified - {client.RemoteEndPoint}"};
            tcpClientManager.ClientDisconnectEvent += ClientDisconnected;
            client.BeginReceive(tcpClientManager.ReceiveBuffer, SocketFlags.None, tcpClientManager.Received, client);

            _clientSockets.Add(tcpClientManager);
        }

        /// <summary>
        /// 客户端断开连接事件
        /// </summary>
        /// <param name="tcpClient"></param>
        private static void ClientDisconnected(TcpClientManager tcpClient)
        {
            AdminReportService.Instance.Info($"客户端连接断开，客户端信息：{tcpClient.ReceiverName}");
        }
    }
}

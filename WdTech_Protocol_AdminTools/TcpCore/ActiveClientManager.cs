using System.Collections.Generic;
using System.Net.Sockets;

namespace WdTech_Protocol_AdminTools.TcpCore
{
    public class ActiveClientManager
    {
        /// <summary>
        /// 客户端连接
        /// </summary>
        private readonly Dictionary<string, TcpClientReceiver> _clientSockets = new Dictionary<string, TcpClientReceiver>();

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

        public void Register()
        {
            
        }
    }
}

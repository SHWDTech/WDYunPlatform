using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using MisakaBanZai.Common;

namespace MisakaBanZai.Services
{
    public class MisakaTcpClient: IMisakaConnection
    {
        /// <summary>
        /// TCP客户端对象
        /// </summary>
        private readonly TcpClient _tcpClient;

        public string ConnectionName { get; set; }

        public string ConnectionType { get; set; }

        public object ConnObject => _tcpClient.Client;

        /// <summary>
        /// 数据接收缓存
        /// </summary>
        public IList<ArraySegment<byte>> ReceiveBuffer { get; } = new List<ArraySegment<byte>>() {new ArraySegment<byte>(new byte[Appconfig.TcpBufferSize])};

        private readonly IList<byte> _processBuffer = new List<byte>();

        public MisakaTcpClient(IPAddress ipAddress, int port)
        {
            _tcpClient = new TcpClient(new IPEndPoint(ipAddress, port));
            _tcpClient.Client.BeginReceive(ReceiveBuffer, SocketFlags.None, Received, _tcpClient);
        }

        public MisakaTcpClient(TcpClient client)
        {
            _tcpClient = client;
            _tcpClient.Client.BeginReceive(ReceiveBuffer, SocketFlags.None, Received, _tcpClient);
        }

        public void Send(byte[] bytes)
        {
            
        }

        /// <summary>
        /// TCP客户端异步接收数据
        /// </summary>
        /// <param name="result"></param>
        public void Received(IAsyncResult result)
        {
            var client = (Socket)result.AsyncState;

            lock (ReceiveBuffer)
            {
                var readCount = client.EndReceive(result);

                var array = ReceiveBuffer.Last().Array;
                for (var i = 0; i < readCount; i++)
                {
                    _processBuffer.Add(array[i]);
                }
            }

            client.BeginReceive(ReceiveBuffer, SocketFlags.None, Received, client);
        }
    }
}

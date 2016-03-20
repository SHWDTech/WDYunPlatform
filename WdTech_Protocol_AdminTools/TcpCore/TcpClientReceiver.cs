using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using WdTech_Protocol_AdminTools.Models;

namespace WdTech_Protocol_AdminTools.TcpCore
{
    /// <summary>
    /// TCP客户端数据接收器
    /// </summary>
    public class TcpClientReceiver
    {
        private readonly ActiveClientManager _manager;

        private readonly Socket _clientSocket;

        /// <summary>
        /// 数据处理缓存
        /// </summary>
        private readonly IList<byte> _processBuffer = new List<byte>();

        //private readonly Thread 

        /// <summary>
        /// 数据接收缓存
        /// </summary>
        public IList<ArraySegment<byte>> ReceiveBuffer { get; set; } = new List<ArraySegment<byte>>();

        /// <summary>
        /// 是否已认定身份
        /// </summary>
        public bool Identified { get; set; } = false;

        /// <summary>
        /// 接收器名称
        /// </summary>
        public string ReceiverName { get; set; }

        /// <summary>
        /// 初始化新的TCP客户端接收器实例
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="clientSocket"></param>
        public TcpClientReceiver(ActiveClientManager manager, Socket clientSocket)
        {
            _manager = manager;
            _clientSocket = clientSocket;
            ReceiveBuffer.Add(new ArraySegment<byte>(new byte[AppConfig.DefaultTcpBufferSize]));
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

        /// <summary>
        /// 关闭TCP客户端连接
        /// </summary>
        public void Close()
        {
            _clientSocket.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace WdTech_Protocol_AdminTools.TcpCore
{
    /// <summary>
    /// 通信服务
    /// </summary>
    public class CommunicationServices
    {
        /// <summary>
        /// 服务启动时间
        /// </summary>
        public static DateTime StartDateTime { get; private set; }

        /// <summary>
        /// 服务是否启动
        /// </summary>
        public static bool IsStart { get; private set; }

        /// <summary>
        /// 设备请求侦测线程
        /// </summary>
        private static readonly Thread ClientDetectiveThread = new Thread(ClientDetective)
        {
            IsBackground = true,
            Name = "ClientDetective"
        };

        /// <summary>
        /// 未确定设备身份的TCP连接
        /// </summary>
        private static readonly Dictionary<string, Thread> UndefineTcpClientThreads = new Dictionary<string, Thread>(); 

        /// <summary>
        /// 服务监听器
        /// </summary>
        private static TcpListener _serverListener;

        public static void Start(IPEndPoint ipEndPoint)
        {
            if (IsStart) return;

            _serverListener = new TcpListener(ipEndPoint) {ExclusiveAddressUse = false};
            _serverListener.Start();

            //在TCP接收线程开始前设置服务器开始标识，否则服务器接收线程无法正常启动
            IsStart = true;
            ClientDetectiveThread.Start();
            StartDateTime = DateTime.Now;
        }

        public static void Stop()
        {
            if (!IsStart) return;

            IsStart = false;
            StartDateTime = DateTime.MinValue;
        }

        /// <summary>
        /// 侦测设备连接请求
        /// </summary>
        public static void ClientDetective()
        {
            while (IsStart)
            {
                var clientAcceptResult = _serverListener.AcceptSocketAsync();
                if (clientAcceptResult.Result != null)
                {
                    
                }
            }
        }
    }
}

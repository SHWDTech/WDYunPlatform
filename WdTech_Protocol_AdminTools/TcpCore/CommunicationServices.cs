using System;
using System.Net;
using System.Net.Sockets;
using SHWDTech.Platform.ProtocolCoding;
using SHWDTech.Platform.Utility;

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
        /// TCP客户端管理器
        /// </summary>
        private static readonly ActiveClientManager Manager;

        /// <summary>
        /// 服务监听器
        /// </summary>
        private static TcpListener _serverListener;

        static CommunicationServices()
        {
            Manager = new ActiveClientManager();
        }

        /// <summary>
        /// 开启服务
        /// </summary>
        /// <param name="ipEndPoint"></param>
        public static void Start(IPEndPoint ipEndPoint)
        {
            if (IsStart) return;

            _serverListener = new TcpListener(ipEndPoint) {ExclusiveAddressUse = false};
            _serverListener.Start();
            _serverListener.BeginAcceptSocket(AcceptClient, _serverListener);

            ProtocolInfoManager.InitManager();

            IsStart = true;
            StartDateTime = DateTime.Now;
        }

        /// <summary>
        /// 停止服务
        /// </summary>
        public static void Stop()
        {
            if (!IsStart) return;

            IsStart = false;
            StartDateTime = DateTime.MinValue;
        }

        /// <summary>
        /// 侦测设备连接请求
        /// </summary>
        public static void AcceptClient(IAsyncResult result)
        {
            var server = (TcpListener)result.AsyncState;

            try
            {

                var client = server.EndAcceptSocket(result);

                Manager.AddClient(client);
            }
            catch (Exception ex)
            {
                LogService.Instance.Warn("接收客户端请求失败！", ex);
            }
            
            server.BeginAcceptSocket(AcceptClient, server);
        }
    }
}

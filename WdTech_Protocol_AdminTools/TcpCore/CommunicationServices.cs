using System;
using System.Net;
using System.Net.Sockets;
using SHWDTech.Platform.ProtocolCoding;
using SHWDTech.Platform.Utility;
using WdTech_Protocol_AdminTools.Services;

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

        /// <summary>
        /// 服务器监听地址
        /// </summary>
        public static IPEndPoint ServerIpEndPoint { get; private set; }

        static CommunicationServices()
        {
            Manager = new ActiveClientManager();
        }

        /// <summary>
        /// 开启服务
        /// </summary>
        /// <param name="ipEndPoint"></param>
        public static bool Start(IPEndPoint ipEndPoint)
        {
            if (IsStart) return true;

            try
            {
                _serverListener = new TcpListener(ipEndPoint) {ExclusiveAddressUse = false};
                ServerIpEndPoint = ipEndPoint;
                _serverListener.Start();
                _serverListener.BeginAcceptSocket(AcceptClient, _serverListener);

                ProtocolInfoManager.InitManager();
                StartDateTime = DateTime.Now;
                IsStart = true;

                ReportService.Instance.Info("服务器启动成功！");
            }
            catch (SocketException ex)
            {
                ReportService.Instance.Warning("服务器启动失败!", ex);
                return false;
            }
            catch (ObjectDisposedException ex)
            {
                ReportService.Instance.Warning("服务器启动侦听失败，套接字已经关闭!", ex);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 停止服务
        /// </summary>
        public static bool Stop()
        {
            if (!IsStart) return true;

            try
            {
                if (_serverListener.Server.Connected)
                {
                    _serverListener.Server.Shutdown(SocketShutdown.Both);
                    _serverListener.Server.Disconnect(false);
                    IsStart = false;
                }

                _serverListener.Server.Close(0);

                StartDateTime = DateTime.MinValue;

                ReportService.Instance.Info("服务器停止成功！");
            }
            catch (Exception ex)
            {
                LogService.Instance.Info("关闭服务器时，发生套接字错误。", ex);
            }
            
            return true;
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

                ReportService.Instance.Info($"客户端连接建立，IP地址：{client.RemoteEndPoint}。");
            }
            catch (ObjectDisposedException ex)
            {
                ReportService.Instance.Info("侦听器已经关闭", ex);
                return;
            }
            catch (SocketException ex)
            {
                ReportService.Instance.Warning("接收客户端请求失败！", ex);
            }

            server.BeginAcceptSocket(AcceptClient, server);
        }
    }
}

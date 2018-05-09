using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using ProtocolServiceAbstraction;
using SHWDTech.Platform.ProtocolCoding;
using SHWDTech.Platform.Utility;
using WdTech_Protocol_AdminTools.Services;

namespace WdTech_Protocol_AdminTools.TcpCore
{
    /// <summary>
    /// 通信服务
    /// </summary>
    public class CommunicationServices : IProtocolService
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

        public static int AliveConnection => Manager.AliveConnection;

        /// <summary>
        /// 服务监听器
        /// </summary>
        private static Socket _serverListener;

        /// <summary>
        /// 服务器监听地址
        /// </summary>
        public static IPEndPoint ServerIpEndPoint { get; private set; }

        private static readonly ManualResetEvent AllDone = new ManualResetEvent(false);

        static CommunicationServices()
        {
            Manager = new ActiveClientManager();
            ProtocolInfoManager.InitManager();
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
                _serverListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _serverListener.Bind(ipEndPoint);
                ServerIpEndPoint = ipEndPoint;
                _serverListener.LingerState = new LingerOption(false, 1);
                _serverListener.Listen(2048);
                _serverListener.BeginAccept(AcceptClient, _serverListener);
                Manager.Start();

                StartDateTime = DateTime.Now;
                IsStart = true;

                AdminReportService.Instance.Info("服务器启动成功！");
            }
            catch (SocketException ex)
            {
                AdminReportService.Instance.Warning("服务器启动失败!", ex);
                return false;
            }
            catch (ObjectDisposedException ex)
            {
                AdminReportService.Instance.Warning("服务器启动侦听失败，套接字已经关闭!", ex);
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
                if (_serverListener.Connected)
                {
                    _serverListener.Shutdown(SocketShutdown.Both);
                    _serverListener.Disconnect(false);
                }

                Manager.Stop();
                _serverListener.Close(0);


                IsStart = false;

                StartDateTime = DateTime.MinValue;

                AdminReportService.Instance.Info("服务器停止成功！");
            }
            catch (Exception ex)
            {
                LogService.Instance.Info("关闭服务器时，发生套接字错误。", ex);
            }

            return true;
        }

        /// <summary>
        /// 服务关闭
        /// </summary>
        public static void Close()
        {
            Stop();
        }

        /// <summary>
        /// 侦测设备连接请求
        /// </summary>
        public static void AcceptClient(IAsyncResult result)
        {
            var server = (Socket)result.AsyncState;

            try
            {
                var client = server.EndAccept(result);

                Manager.AddClient(client);

                AdminReportService.Instance.Info($"客户端连接建立，IP地址：{client.RemoteEndPoint}。");
            }
            catch (ObjectDisposedException ex)
            {
                AdminReportService.Instance.Info("侦听器已经关闭", ex);
                return;
            }
            catch (SocketException ex)
            {
                AdminReportService.Instance.Warning("接收客户端请求失败！", ex);
                return;
            }

            server.BeginAccept(AcceptClient, server);

            AllDone.Set();
        }

        public void Send(byte[] data, Guid device)
        {
            Manager.SendDatas(data, device);
        }
    }
}

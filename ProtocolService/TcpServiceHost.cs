﻿using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Timers;
using Timer = System.Timers.Timer;

namespace SHWDTech.Platform.ProtocolService
{
    public class TcpServiceHost : ServiceHost
    {
        /// <summary>
        /// 接受客户端连接事件
        /// </summary>
        public event ActiveClientEventHandler ClientAccepted;

        /// <summary>
        /// 接收客户端连接失败
        /// </summary>
        public event ActiveClientEventHandler ClientAcceptFailed;

        /// <summary>
        /// 客户端接收缓存大小
        /// </summary>
        public int ClientReceiveBufferSize = 4096;

        /// <summary>
        /// 连接超时断开周期
        /// </summary>
        public TimeSpan DisconnectInterval { get; set; } = TimeSpan.FromMinutes(5);

        /// <summary>
        /// 连接检查时间间隔
        /// </summary>
        public double CheckInterval { get; set; } = 120000;

        /// <summary>
        /// 连接检查定时器
        /// </summary>
        public readonly Timer ConnectionCheckTimer = new Timer();

        /// <summary>
        /// TCP重置事件
        /// </summary>
        private static readonly ManualResetEvent AllDone = new ManualResetEvent(false);

        public TcpServiceHost()
        {
            ConnectionCheckTimer.Elapsed += ConnectionCheck;
            ConnectionCheckTimer.Interval = CheckInterval;
        }

        /// <summary>
        /// 设置TCP连接的本地参数
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <param name="port"></param>
        public virtual void SetupTcpAddressPort(string ipAddress, int port)
        {
            var endPoint = new IPEndPoint(IPAddress.Parse(ipAddress), port);
            SetupTcpAddressPort(endPoint);
        }

        /// <summary>
        /// 设置TCP连接的本地参数
        /// </summary>
        /// <param name="endPoint"></param>
        public virtual void SetupTcpAddressPort(IPEndPoint endPoint)
        {
            if(Status != ServiceHostStatus.Stopped) throw new InvalidOperationException("服务启动后不允许重新设置参数信息。");
            HostSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            HostSocket.Bind(endPoint);
        }

        public override void Start()
        {
            if (HostSocket == null) throw new InvalidOperationException("尚未设置监听端口信息。");
            HostSocket.LingerState = new LingerOption(false, 1);
            HostSocket.Listen(2048);
            HostSocket.BeginAccept(AcceptClient, HostSocket);
            StartDateTime = DateTime.Now;
            ConnectionCheckTimer.Enabled = true;
            Status = ServiceHostStatus.Running;
        }

        public override void Close()
        {
            if(Status == ServiceHostStatus.Stopped) throw new InvalidOperationException("服务已经关闭。");
            Status = ServiceHostStatus.Stopped;
            HostSocket.Close();
            HostSocket.Dispose();
            ConnectionCheckTimer.Enabled = false;
            while (ActiveClients.Count > 0)
            {
                var client = (SocketActiveClient)ActiveClients[0];
                client.Close();
            }
            ActiveClients.Clear();
            HostSocket = null;
        }

        public override void Restart()
        {
            
        }

        public override void RemoveClient(IActiveClient client)
        {
            lock (ActiveClients)
            {
                if (ActiveClients.Any(c => c.ClientGuid == client.ClientGuid))
                {
                    ActiveClients.Remove(client);
                }
            }
        }

        protected virtual void AcceptClient(IAsyncResult result)
        {
            var server = (Socket)result.AsyncState;

            try
            {
                if (Status != ServiceHostStatus.Running)
                {
                    return;
                }
                var client = new SocketActiveClient(server.EndAccept(result), this);
                AddClient(client);
                OnClientAccedped(client);
            }
            catch (Exception ex)
            {
                OnClientAcceptFailed(ex);
                Restart();
                return;
            }

            server.BeginAccept(AcceptClient, server);
            AllDone.Set();
        }

        protected virtual void AddClient(IActiveClient client)
        {
            lock (ActiveClients)
            {
                ActiveClients.Add(client);
            }
        }

        /// <summary>
        /// 触发客户端连接接受事件
        /// </summary>
        /// <param name="client"></param>
        protected virtual void OnClientAccedped(IActiveClient client)
        {
            ClientAccepted?.Invoke(new ActiveClientEventArgs(client));
        }

        /// <summary>
        /// 触发客户端连接接收失败事件
        /// </summary>
        /// <param name="ex"></param>
        protected virtual void OnClientAcceptFailed(Exception ex)
        {
            ClientAcceptFailed?.Invoke(new ActiveClientEventArgs(null, ex));
        }

        protected void ConnectionCheck(object sender, ElapsedEventArgs e)
        {
            lock (ActiveClients)
            {
                var checkTime = DateTime.Now;
                for (var i = 0; i < ActiveClients.Count; i++)
                {
                    var activeClient = ActiveClients[i];
                    if (checkTime - activeClient.LastAliveDateTime <= DisconnectInterval) continue;
                    var client = (SocketActiveClient)activeClient;
                    client.Close();
                    ActiveClients.Remove(activeClient);
                }
            }
        }
    }
}

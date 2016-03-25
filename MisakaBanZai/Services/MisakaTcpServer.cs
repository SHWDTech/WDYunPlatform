﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using MisakaBanZai.Common;
using MisakaBanZai.Enums;
using MisakaBanZai.Models;
using SHWDTech.Platform.Utility;

namespace MisakaBanZai.Services
{
    /// <summary>
    /// TCP服务器
    /// </summary>
    public class MisakaTcpServer : IMisakaConnection
    {
        /// <summary>
        /// TCP传入连接监听对象
        /// </summary>
        private Socket _tcpListener;

        /// <summary>
        /// 已经接入的TCP连接
        /// </summary>
        private readonly Dictionary<string, MisakaTcpClient> _tcpClients = new Dictionary<string, MisakaTcpClient>();

        public event ClientReceivedDataEventHandler ClientReceivedDataEvent;

        public event ClientDisconnectEventHandler ClientDisconnectEvent;

        /// <summary>
        /// 是否向所有子TCP连接广播
        /// </summary>
        private bool _broadcast;

        /// <summary>
        /// 数据接收缓存
        /// </summary>
        public IList<ArraySegment<byte>> ReceiveBuffer { get; }
            = new List<ArraySegment<byte>>() { new ArraySegment<byte>(new byte[Appconfig.TcpBufferSize]) };

        /// <summary>
        /// 数据处理缓存
        /// </summary>
        public IList<byte> ProcessBuffer { get; } = new List<byte>();

        /// <summary>
        /// 当前选中的客户端
        /// </summary>
        private MisakaTcpClient _currenTcpClient;

        public IMisakaConnectionManagerWindow ParentWindow { get; set; }

        /// <summary>
        /// 客户端接入事件
        /// </summary>
        public event ClientAcceptEventHandler ClientAccept;

        public string ConnectionName => $"{IpAddress}:{Port}";

        public string ConnectionType { get; set; }

        public object ConnObject => _tcpListener;

        public string IpAddress { get; private set; }

        public int Port { get; private set; }

        public bool IsConnected { get; private set; }

        public MisakaTcpServer(IPAddress ipaddress, int port)
        {
            IpAddress = $"{ipaddress}";
            Port = port;
        }

        /// <summary>
        /// 开始侦听TCP传入连接
        /// </summary>
        public bool Connect(IPAddress ipaddress, int port)
        {
            try
            {
                _tcpListener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _tcpListener.Bind(new IPEndPoint(ipaddress, port));
                IpAddress = $"{ipaddress}";
                Port = port;
                _tcpListener.LingerState = new LingerOption(false, 1);
                _tcpListener.Listen(2048);
                _tcpListener.BeginAccept(AcceptClient, _tcpListener);
                IsConnected = true;
            }
            catch (Exception ex)
            {
                ParentWindow.DispatcherAddReportData(ReportMessageType.Error, "启动侦听失败！");
                LogService.Instance.Error("启动侦听失败！", ex);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 侦测设备连接请求
        /// </summary>
        private void AcceptClient(IAsyncResult result)
        {
            var server = (Socket)result.AsyncState;

            try
            {
                var client = server.EndAccept(result);
                var misakaClient = new MisakaTcpClient(client) { ParentWindow = ParentWindow, ConnectionType = ConnectionItemType.TcpAcceptedClient };
                misakaClient.ClientBeginReceive();
                misakaClient.ClientReceivedDataEvent += OnClientReceivedData;
                misakaClient.ClientDisconnectEvent += OnClientDisconnect;
                _tcpClients.Add(client.RemoteEndPoint.ToString(), misakaClient);
                ParentWindow.DispatcherAddReportData(ReportMessageType.Info, $"添加客户端成功。{client.RemoteEndPoint}");
                OnClientAccepted(EventArgs.Empty);
            }
            catch (ObjectDisposedException ex)
            {
                LogService.Instance.Info("侦听器已经关闭", ex);
                return;
            }
            catch (Exception ex)
            {
                LogService.Instance.Warn("接收客户端请求失败！", ex);
                ParentWindow.DispatcherAddReportData(ReportMessageType.Error, "接收客户端请求失败！");
            }

            server.BeginAccept(AcceptClient, server);
        }

        /// <summary>
        /// 设置当前客户端连接
        /// </summary>
        public void SetCurrentClient(string clientName)
        {
            if (clientName == Appconfig.SelectAllConnection)
            {
                _broadcast = true;
                return;
            }
            _currenTcpClient = _tcpClients[clientName];
        }

        /// <summary>
        /// 向客户端发送数据
        /// </summary>
        /// <param name="bytes"></param>
        public int Send(byte[] bytes)
        {
            var count = 0;
            if (!_broadcast)
            {
                return _currenTcpClient.Send(bytes);
            }

            foreach (var tcpClient in _tcpClients)
            {
                count = tcpClient.Value.Send(bytes);
            }

            return count;
        }

        public bool Close()
        {
            try
            {
                if (_tcpListener.Connected)
                {
                    _tcpListener.Shutdown(SocketShutdown.Both);
                    _tcpListener.Disconnect(false);
                    IsConnected = false;
                }
                _tcpListener.Close(50);
                foreach (var misakaTcpClient in _tcpClients)
                {
                    misakaTcpClient.Value.Close();
                }

            }
            catch (Exception ex)
            {
                LogService.Instance.Error("关闭套接字错误。", ex);
                return false;
            }

            return true;
        }

        /// <summary>
        /// 当接收到客户端连接时
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnClientAccepted(EventArgs e)
        {
            ClientAccept?.Invoke(this, e);
        }

        /// <summary>
        /// 获取连接客户端的名称
        /// </summary>
        /// <returns></returns>
        public List<string> GetClientNameList()
            => _tcpClients.Select(misakaTcpClient => misakaTcpClient.Key).ToList();

        /// <summary>
        /// 数据接收时出发
        /// </summary>
        private void OnReceivedData()
        {
            ClientReceivedDataEvent?.Invoke(this);
        }

        /// <summary>
        /// 客户端接收数据事件
        /// </summary>
        /// <param name="conn"></param>
        private void OnClientReceivedData(IMisakaConnection conn)
        {
            foreach (var socketByte in conn.OutPutSocketBytes())
            {
                ProcessBuffer.Add(socketByte);
            }
            OnReceivedData();
        }

        /// <summary>
        /// 客户端断开连接事件
        /// </summary>
        /// <param name="conn"></param>
        private void OnClientDisconnect(IMisakaConnection conn)
        {
            if (_tcpClients.ContainsKey(conn.ConnectionName))
                _tcpClients.Remove(conn.ConnectionName);

            var address = ((IPEndPoint)((Socket)conn.ConnObject).RemoteEndPoint).ToString().Split(':');
            ParentWindow.DispatcherAddReportData(ReportMessageType.Info, $"与客户端的连接断开{address[0]}:{address[1]}");
        }

        public byte[] OutPutSocketBytes()
        {
            byte[] receivedData;

            lock (ProcessBuffer)
            {
                receivedData = ProcessBuffer.ToArray();
                for (var i = 0; i < receivedData.Length; i++)
                {
                    ProcessBuffer.RemoveAt(0);
                }
            }
            return receivedData;
        }
    }
}

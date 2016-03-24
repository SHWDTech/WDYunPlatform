using System;
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
        private readonly TcpListener _tcpListener;

        /// <summary>
        /// 已经接入的TCP连接
        /// </summary>
        private readonly Dictionary<string, MisakaTcpClient> _tcpClients = new Dictionary<string, MisakaTcpClient>();

        /// <summary>
        /// 客户端数据接收事件
        /// </summary>
        public event ClientReceivedDataEventHandler ClientReceivedDataEvent;

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

        public string ConnectionName { get; set; }

        public string ConnectionType { get; set; }

        public object ConnObject => _tcpListener;

        /// <summary>
        /// 侦听服务已经启动
        /// </summary>
        public bool IsStarted { get; private set; }

        public MisakaTcpServer(IPAddress ipaddress, int port)
        {
            _tcpListener = new TcpListener(ipaddress, port);
            ConnectionName = $"{ipaddress}:{port}";
        }

        /// <summary>
        /// 开始侦听TCP传入连接
        /// </summary>
        public void Start()
        {
            _tcpListener.Start();
            _tcpListener.BeginAcceptTcpClient(AcceptClient, _tcpListener);
            IsStarted = true;
        }

        /// <summary>
        /// 侦测设备连接请求
        /// </summary>
        private void AcceptClient(IAsyncResult result)
        {
            var server = (TcpListener)result.AsyncState;

            try
            {
                var client = server.EndAcceptTcpClient(result);
                var misakaClient = new MisakaTcpClient(client) {ParentWindow = ParentWindow };
                misakaClient.ClientBeginReceive();
                misakaClient.ClientReceivedDataEvent += OnClientReceivedData;
                misakaClient.ClientDisconnectEvent += OnClientDisconnect;
                _tcpClients.Add(client.Client.RemoteEndPoint.ToString(), misakaClient);
                ParentWindow.DispatcherAddReportData(ReportMessageType.Info, $"添加客户端成功。{client.Client.RemoteEndPoint}");
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

            server.BeginAcceptSocket(AcceptClient, server);
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

        public void Close()
        {
            try
            {
                if (_tcpClients.Count > 0)
                {
                    _tcpListener.Server.Shutdown(SocketShutdown.Both);
                }
                _tcpListener.Server.Close(50);
            }
            catch (Exception ex)
            {
                LogService.Instance.Error("关闭套接字错误。", ex);
            }
            foreach (var misakaTcpClient in _tcpClients)
            {
                misakaTcpClient.Value.Close();
            }
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
            if(_tcpClients.ContainsKey(conn.ConnectionName))
            _tcpClients.Remove(conn.ConnectionName);
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

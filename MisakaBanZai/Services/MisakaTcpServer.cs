using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using MisakaBanZai.Common;
using MisakaBanZai.Enums;
using MisakaBanZai.Models;
using MisakaBanZai.Views;
using SHWDTech.Platform.Utility;
using SHWDTech.Platform.Utility.Enum;

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

        public event DataSendEventHandler DataSendEvent;

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

        public string TargetConnectionName { get; set; }

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
                TargetConnectionName = string.Empty;
                _tcpListener.LingerState = new LingerOption(false, 1);
                _tcpListener.Listen(2048);
                _tcpListener.BeginAccept(AcceptClient, _tcpListener);
                IsConnected = true;
            }
            catch (Exception ex)
            {
                ParentWindow.DispatcherAddReportData(ReportMessageType.Error, ReportMessageEnum.StartListenerFailed);
                LogService.Instance.Error(ReportMessageEnum.StartListenerFailed, ex);
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
                ParentWindow.DispatcherAddReportData(ReportMessageType.Info, $"{ReportMessageEnum.AcceptClientSuccess}{client.RemoteEndPoint}");
                OnClientAccepted(EventArgs.Empty);
            }
            catch (ObjectDisposedException ex)
            {
                LogService.Instance.Info(ReportMessageEnum.ListenerClosed, ex);
                OnServerDisconnected();
                return;
            }
            catch (Exception ex)
            {
                LogService.Instance.Warn(ReportMessageEnum.AcceptClientFailed, ex);
                ParentWindow.DispatcherAddReportData(ReportMessageType.Error, ReportMessageEnum.AcceptClientFailed);
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

            _broadcast = false;
            _currenTcpClient = _tcpClients[clientName];
        }

        /// <summary>
        /// 向客户端发送数据
        /// </summary>
        /// <param name="bytes"></param>
        public int Send(byte[] bytes)
        {
            var count = 0;
            try
            {
                if (!_broadcast)
                {
                    count = _currenTcpClient.Send(bytes);
                }
                else
                {
                    foreach (var tcpClient in _tcpClients)
                    {
                        count = tcpClient.Value.Send(bytes);
                    }
                }

                OnDataSend(count);
            }
            catch (Exception ex)
            {
                LogService.Instance.Error(ReportMessageEnum.DataSendFailed, ex);
                return 0;
            }

            return count;
        }

        /// <summary>
        /// 当发送数据时触发事件
        /// </summary>
        /// <param name="count"></param>
        private void OnDataSend(int count)
        {
            DataSendEvent?.Invoke(count);
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

                _tcpListener.Close(0);

                foreach (var misakaTcpClient in _tcpClients)
                {
                    misakaTcpClient.Value.Close();
                }
            }
            catch (Exception ex)
            {
                LogService.Instance.Error(ReportMessageEnum.CloseSocketException, ex);
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
        public List<string> GetClientNameList() => _tcpClients.Select(misakaTcpClient => misakaTcpClient.Key).ToList();

        /// <summary>
        /// 触发数据接收事件
        /// </summary>
        private void OnReceivedData(IMisakaConnection conn)
        {
            ClientReceivedDataEvent?.Invoke(conn);
        }

        /// <summary>
        /// 客户端接收数据事件
        /// </summary>
        /// <param name="conn"></param>
        private void OnClientReceivedData(IMisakaConnection conn)
        {
            OnReceivedData(conn);
        }

        /// <summary>
        /// 触发服务器断开连接事件
        /// </summary>
        private void OnServerDisconnected()
        {
            ClientDisconnectEvent?.Invoke(this);
        }

        /// <summary>
        /// 客户端断开连接事件
        /// </summary>
        /// <param name="conn"></param>
        private void OnClientDisconnect(IMisakaConnection conn)
        {
            if (_tcpClients.ContainsKey(conn.ConnectionName))
                _tcpClients.Remove(conn.ConnectionName);

            ParentWindow.DispatcherAddReportData(ReportMessageType.Info, $"{ReportMessageEnum.ClientDisconnected}：{conn.IpAddress}:{conn.Port}");
            ((TcpConnectionView)ParentWindow).OnServerClientDisconnect();
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

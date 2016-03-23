using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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

        public event ClientReceivedDataEventHandler ClientReceivedDataEvent;

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

        public void Stop()
        {
            _tcpListener.Stop();
            IsStarted = false;
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
                var misakaClient = new MisakaTcpClient(client);
                misakaClient.ClientReceivedDataEvent += OnClientReceivedData;
                _tcpClients.Add(client.Client.RemoteEndPoint.ToString(), misakaClient);
                ParentWindow.DispatcherAddReportData($"添加客户端成功。{client.Client.RemoteEndPoint}");
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
                ParentWindow.DispatcherAddReportData("接收客户端请求失败！");
                return;
            }

            server.BeginAcceptSocket(AcceptClient, server);
        }

        /// <summary>
        /// 设置当前客户端连接
        /// </summary>
        public void SetCurrentClient(string clientName)
        {
            _currenTcpClient = _tcpClients[clientName];
        }

        /// <summary>
        /// 向客户端发送数据
        /// </summary>
        /// <param name="bytes"></param>
        public void Send(byte[] bytes)
        {
            _currenTcpClient.Send(bytes);
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


        private void OnClientReceivedData(IMisakaConnection conn)
        {
            foreach (var socketByte in conn.OutPutSocketBytes())
            {
                ProcessBuffer.Add(socketByte);
            }
            OnReceivedData();
        }

        /// <summary>
        /// 数据接收时出发
        /// </summary>
        private void OnReceivedData()
        {
            ClientReceivedDataEvent?.Invoke(this);
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

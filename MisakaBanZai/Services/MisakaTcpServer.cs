using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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
        /// 当前选中的客户端
        /// </summary>
        private MisakaTcpClient _currenTcpClient;

        /// <summary>
        /// 客户端接入事件
        /// </summary>
        public event ClientAcceptEventHandler ClientAccept;

        public string ConnectionName { get; set; }

        public string ConnectionType { get; set; }

        public object ConnObject => _tcpListener; 

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
                _tcpClients.Add(client.Client.RemoteEndPoint.ToString(), new MisakaTcpClient(client));
                OnClientAccepted(EventArgs.Empty);
            }
            catch (Exception ex)
            {
                LogService.Instance.Warn("接收客户端请求失败！", ex);
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
    }
}
